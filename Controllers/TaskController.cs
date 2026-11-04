using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.DTOs;
using TodoApi.Repository;
using MapsterMapper;
namespace TodoApi.Controllers
{
        
    [Route("api/[controller]")]
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
        public async Task<ActionResult<List<TaskResponseDto>>> GetallTasks()
        {
            var res = await repository.GetAllTasksAsync();

            return Ok(mapper.Map<List<TaskResponseDto>>(res));
        }
        [HttpGet("{id:guid}", Name = "GetTaskById")]
        public async Task<ActionResult<TaskItem>> GetTaskById(Guid id)
        {
            var res = await repository.GetTaskByIdAsync(id);
           
            return res is null ? NotFound() : Ok(res);
        }
        [HttpPost(Name = "CreateTask")]
        public async Task<ActionResult<TaskItem>> CreateTask([FromBody] WriteTaskDto taskDto)
        {
            var createdTask = await repository.CreateTaskAsync(taskDto);
            
            return CreatedAtRoute("GetTaskById", new {id = createdTask.Id}, createdTask);
        }
        [HttpPut("{id:guid}", Name = "UpdateTask")]
        public async Task<IActionResult> UpdateTask([FromRoute]Guid id, [FromBody] WriteTaskDto taskDto)
        {
            var res = await repository.UpdateTaskAsync(id, taskDto);
            
            return res is null ? NotFound() : NoContent();
        }
        [HttpDelete("{id:guid}", Name = "DeleteTask")]
        public async Task<IActionResult> DeleteTask([FromRoute]Guid id)
        {
            var res = await repository.DeleteTaskAsync(id);

            return res is null ? NotFound() : NoContent();
        }
    }
}
