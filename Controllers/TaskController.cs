

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoApi.DTOs;
using MapsterMapper;
using TodoApi.Repository;
namespace TodoApi.Controllers
{
        
    [Route("api/[Controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {

        private readonly ITaskRepository repository;
        private readonly IMapper mapper;

        public TaskController(ITaskRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet(Name = "GetAllTasks")]
        public async Task<ActionResult<List<TaskItem>>> GetallTasks()
        {
            var res = await repository.GetAllTasksAsync();

            return Ok(res);
        }
        [HttpGet("{id}", Name = "GetTaskById")]
        public async Task<ActionResult<TaskItem>> GetTaskById(int id)
        {
            var res = new TaskItem();
            try
            {
                res = await repository.GetTaskByIdAsync(id);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return Ok(res);
        }
        [HttpPost(Name = "CreateTask")]
        public async Task<ActionResult<TaskItem>> CreateTask([FromBody] WriteTaskDto taskDto)
        {
            var task = mapper.Map<TaskItem>(taskDto);
            var createdTask = await repository.CreateTaskAsync(task);
            
            return CreatedAtRoute("GetTaskById", new {id = createdTask.Id}, createdTask);
        }
        [HttpPut("{id}", Name = "UpdateTask")]
        public async Task<IActionResult> UpdateTask([FromRoute]int id, [FromBody] WriteTaskDto taskDto)
        {
            var newTask = mapper.Map<TaskItem>(taskDto);
            try
            {
                await repository.UpdateTaskAsync(newTask, id);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            return NoContent();
        }
        [HttpDelete("{id}", Name = "DeleteTask")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
                await repository.DeleteTaskAsync(id);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
            return NoContent();
        }
    }
}
