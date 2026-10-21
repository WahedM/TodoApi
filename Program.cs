using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(option => option.UseInMemoryDatabase("TodoDatabase"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapGet("/", () => "Hello World");

app.MapGet("/GetTasks", (AppDbContext db) =>
{
   var res = db.Tasks.ToList();
   return Results.Ok(res);
});

app.MapGet("/GetTaskById/{id}", (AppDbContext db, int id) =>
{
    var res = db.Tasks.Find(id);
    return Results.Ok(res);
});

app.MapPost("/CreateTasks", (Task task, AppDbContext db) =>
{
    db.Tasks.Add(task);
    db.SaveChanges();
    return Results.Created($"/todoitems/{task.Id}", task);
});

app.MapPut("/UpdateTask/{id}",(AppDbContext db, int id, Task newTask) =>
{
    var task = db.Tasks.Find(id);
    task.Id = newTask.Id;
    task.Title = newTask.Title;
    task.Status = newTask.Status;
    db.SaveChanges();   
    return Results.NoContent();
});

app.MapDelete("/deleteTask/{id}",(int id, AppDbContext db) =>
{
    var task = db.Tasks.Find(id);   
    db.Tasks.Remove(task);
    db.SaveChanges();
    return Results.NoContent();
});
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
