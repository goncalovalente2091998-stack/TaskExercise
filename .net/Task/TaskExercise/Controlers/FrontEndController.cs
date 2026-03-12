using Microsoft.AspNetCore.Mvc;
using TaskExercise.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskExercise.Models;

namespace TaskExercise.Controllers
{
    [ApiController]
    [Route("frontend/tasks")]
    public class FrontEndController : ControllerBase
    {
        private readonly TaskServices _service;

        public FrontEndController(TaskServices service)
        {
            _service = service;
        }

        // GET /frontend/tasks
        [HttpGet]
        public async Task<IEnumerable<Dto.FrontendTaskDto>> Get()
        {
            return await _service.GetTasksAsync();
        }

     
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Dto.CreateTaskRequest dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Title))
            {
                return BadRequest(new { message = "Title is required" });
            }

            await _service.CreateTaskAsync(dto.Title);
            return Ok(new { message = "Task created" });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Dto.UpdateTaskDto dto)
        {
            if (dto == null) return BadRequest();

            await _service.UpdateTaskAsync(id, dto.IsCompleted);
            return Ok(new { message = "Task updated" });
        }
    }


  

}