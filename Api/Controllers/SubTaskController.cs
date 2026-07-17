using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_gerenciamento_tarefas.Application.Features.SubTasks.DTO;
using api_gerenciamento_tarefas.Application.Features.SubTasks.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_gerenciamento_tarefas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SubTaskController : ControllerBase
    {
        private readonly SubTaskService _subTaskService;

        public SubTaskController(SubTaskService subTaskService)
        {
            _subTaskService = subTaskService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var subTask = await _subTaskService.GetByIdAsync(id);
            return Ok(subTask);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var subTasks = await _subTaskService.GetAllAsync();
            return Ok(subTasks);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateSubtaskDto dto)
        {
            var subTask = await _subTaskService.AddAsync(dto);
            return Ok(subTask);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateSubtaskDto dto, Guid id)
        {
            if (id != dto.Id)
                return BadRequest("O ID da sub-tarefa na URL deve corresponder ao ID no corpo da requisição.");

            await _subTaskService.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _subTaskService.DeleteAsync(id);
            return NoContent();
        }
    }
}