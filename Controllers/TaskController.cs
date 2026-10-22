

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace TodoApi.Controllers
{
        
    [Route("api/[Controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly AppDbContext _context;
        public TaskController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult GetTasks()
        {
            var res = _context.Tasks.ToList();    

            return Ok(res);
        }
        [HttpGet("{id}", Name = "GetTaskById")]
        public ActionResult GetTaskbyId(int id)
        {
            var res = _context.Tasks.Find(id);    

            return Ok(res);
        }
        [HttpPost]
        public ActionResult CreateTask(Task task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
            return CreatedAtRoute("GetTaskById", new {id = task.Id}, task);
        }
        [HttpPut("{id}", Name = "UpdateTask")]
        public ActionResult UpdateTask([FromRoute]int id, [FromBody] Task newTask)
        {
            var oldTask = _context.Tasks.Find(id);
            if(oldTask is null) 
                return NotFound("No Task with the given id");
            oldTask.Id = newTask.Id;
            oldTask.Title = newTask.Title;
            oldTask.Status = newTask.Status;
            _context.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id}", Name = "DeleteTask")]
        public ActionResult DeleteTask(int id)
        {
            var res = _context.Tasks.Find(id);
            if(res is null)
                return NotFound("No task with the given id");
            _context.Tasks.Remove(res);
            _context.SaveChanges();
            return NoContent();

        }
    }
}