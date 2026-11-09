using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<PagedResult<TaskResponseDto>>> GetallTasks([FromQuery] TaskQueryParameters query)
        {
            var result = await repository.GetAllTasksAsync(query);

            return Ok(new PagedResult<TaskResponseDto>
            {
                Items = mapper.Map<List<TaskResponseDto>>(result.Items),
                Page = result.Page,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount
            });
        }
        [HttpGet("{id:guid}", Name = "GetTaskById")]
        public async Task<ActionResult<TaskResponseDto>> GetTaskById(Guid id)
        {
            var res = await repository.GetTaskByIdAsync(id);
           
            return res is null ? NotFound() : Ok(mapper.Map<TaskResponseDto>(res));
        }
        [HttpPost(Name = "CreateTask")]
        public async Task<ActionResult<TaskResponseDto>> CreateTask([FromBody] WriteTaskDto taskDto)
        {
            var createdTask = await repository.CreateTaskAsync(taskDto);
            
            return CreatedAtRoute("GetTaskById", new {id = createdTask.Id}, mapper.Map<TaskResponseDto>(createdTask));
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
