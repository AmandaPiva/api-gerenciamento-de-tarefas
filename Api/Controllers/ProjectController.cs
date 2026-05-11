using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_gerenciamento_tarefas.Application.Features.Projects.DTO;
using api_gerenciamento_tarefas.Application.Features.Projects.Services;
using api_gerenciamento_tarefas.Application.Features.Tasks.DTO;
using Microsoft.AspNetCore.Mvc;

namespace api_gerenciamento_tarefas.Api.Controllers
{
    [ApiController]
    [Route("api/Projects/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectService _projectService;

        public ProjectController(ProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var project = await _projectService.GetByIdAsync(id);
            return Ok(project);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var projects = await _projectService.GetAllAsync();
            return Ok(projects);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateProjectDto dto)
        {
            var project = await _projectService.AddAsync(dto);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = project.Id }, project);
        }

        [HttpPut("/UpdateProject/{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateProjectDto dto, Guid id)
        {
            if (id != dto.Id)
                return BadRequest("O ID do projeto na URL deve corresponder ao ID no corpo da requisição.");

            await _projectService.UpdateAsync(dto);
            return NoContent();
        }

        [HttpPost("/AddTaskToProject/{projectId}")]
        public async Task<IActionResult> AddTaskToProjectAsync(Guid projectId, [FromBody] CreateTaskItemDto dto)
        {
            var task = await _projectService.AddTaskToProjectAsync(projectId, dto);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = task.Id }, task);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _projectService.DeleteAsync(id);
            return NoContent();
        }


    }
}