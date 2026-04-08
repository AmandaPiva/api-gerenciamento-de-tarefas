using System;
using System.Collections.Generic;
using System.Linq;
using System;
using api_gerenciamento_tarefas.Domain.Entities;


namespace api_gerenciamento_tarefas.Application.Features.Projects.Interfaces
{
    public interface IProjectRepository
    {
        public Task<Project?> GetByIdAsync(Guid id);
        public Task<List<Project>> GetAllAsync();
        public Task AddAsync(Project project);
        public Task UpdateAsync(Project project);
        public Task DeleteAsync(Guid id);
        public Task<TaskItem?> AddTaskToProjectAsync(Guid projectId, TaskItem task);
    }
}