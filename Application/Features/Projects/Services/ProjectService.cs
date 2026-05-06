using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_gerenciamento_tarefas.Application.Features.Projects.DTO;
using api_gerenciamento_tarefas.Application.Features.Projects.Interfaces;
using api_gerenciamento_tarefas.Application.Features.Tasks.DTO;
using api_gerenciamento_tarefas.Application.Interfaces;
using api_gerenciamento_tarefas.Domain.Entities;

namespace api_gerenciamento_tarefas.Application.Features.Projects.Services
{
    public class ProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProjectResponseDto?> GetByIdAsync(Guid id)
        {
            var project = await _unitOfWork.ProjectRepository.GetByIdAsync(id);
            if (project == null)
                throw new Exception("Projeto não encontrado.");
                
            return new ProjectResponseDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                CreationDate = project.CreationDate,
                CompletionDate = project.CompletionDate,
                Completed = project.Completed
            };
        }

        public async Task<List<ProjectResponseDto>> GetAllAsync()
        {
            var projects = await _unitOfWork.ProjectRepository.GetAllAsync();
            return projects.Select(p => new ProjectResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CreationDate = p.CreationDate,
                CompletionDate = p.CompletionDate,
                Completed = p.Completed
            }).ToList();
        }

        public async Task<Project> AddAsync(CreateProjectDto dto)
        {
            var project = new Project
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                CreationDate = DateTime.UtcNow,
                CompletionDate = dto.CompletionDate,
                Completed = dto.Completed
            };
            if (string.IsNullOrWhiteSpace(project.Name))
                throw new Exception("O nome do projeto é obrigatório.");

            await _unitOfWork.ProjectRepository.AddAsync(project);
            await _unitOfWork.SaveChangesAsync();

            return project;
        }

        public async Task UpdateAsync(UpdateProjectDto dto)
        {
            var existingProject = await _unitOfWork.ProjectRepository.GetByIdAsync(dto.Id);
            if (existingProject == null)
                throw new Exception("Projeto não encontrado.");
           
            existingProject.Name = dto.Name;
            existingProject.Description = dto.Description;
            existingProject.CompletionDate = dto.CompletionDate;
            existingProject.Completed = dto.Completed;

            await _unitOfWork.ProjectRepository.UpdateAsync(existingProject);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<TaskItem> AddTaskToProjectAsync(Guid projectId, CreateTaskItemDto taskItem)
        {
            var task = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = taskItem.Title,
                Description = taskItem.Description,
                CreationDate = DateTime.UtcNow,
                CompletionDate = taskItem.CompletionDate,
                Completed = taskItem.Completed,
                IsPriority = taskItem.IsPriority,
            };

            if (string.IsNullOrWhiteSpace(task.Title))
                throw new Exception("O título da tarefa é obrigatório.");

            var addedTask = await _unitOfWork.ProjectRepository.AddTaskToProjectAsync(projectId, task);
            if (addedTask == null)
                throw new Exception("Projeto não encontrado para adicionar a tarefa.");

            await _unitOfWork.SaveChangesAsync();
            return addedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var project = await _unitOfWork.ProjectRepository.GetByIdAsync(id);
            if (project == null)
                throw new Exception("Projeto não encontrado.");

            await _unitOfWork.ProjectRepository.DeleteAsync(project);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}