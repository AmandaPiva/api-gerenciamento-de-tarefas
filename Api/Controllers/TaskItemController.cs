using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using api_gerenciamento_tarefas.Application.Features.Tasks.DTO;
using api_gerenciamento_tarefas.Application.Features.Tasks.Interfaces;

namespace api_gerenciamento_tarefas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TaskItemController : ControllerBase
    {
        private readonly ITaskItemService _taskItemService;

        public TaskItemController(ITaskItemService taskItemService)
        {
            _taskItemService = taskItemService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var taskItem = await _taskItemService.GetByIdAsync(id);
            return Ok(taskItem);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var taskItems = await _taskItemService.GetAllAsync();
            return Ok(taskItems);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateTaskItemDto dto)
        {
            var taskItem = await _taskItemService.AddAsync(dto);
            return Ok(taskItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateTaskItemDto dto, Guid id)
        {
            if (id != dto.Id)
            {
                return BadRequest("O ID da tarefa na URL deve corresponder ao ID no corpo da requisição.");
            }

            await _taskItemService.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _taskItemService.DeleteAsync(id);
            return NoContent();
        }
    }
}