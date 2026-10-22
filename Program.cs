using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(option => option.UseInMemoryDatabase("TodoDatabase"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


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
    if(task is null)
        return Results.NotFound();
    task.Id = newTask.Id;
    task.Title = newTask.Title;
    task.Status = newTask.Status;
    db.SaveChanges();   
    return Results.NoContent();
});

app.MapDelete("/deleteTask/{id}",(int id, AppDbContext db) =>
{
    var task = db.Tasks.Find(id);   
    if(task is null)
        return Results.NotFound();
    db.Tasks.Remove(task);
    db.SaveChanges();
    return Results.NoContent();
});
app.MapControllers();
app.Run();


