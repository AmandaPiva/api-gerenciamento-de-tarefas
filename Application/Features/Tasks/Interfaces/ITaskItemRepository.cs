using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_gerenciamento_tarefas.Domain.Entities;

namespace api_gerenciamento_tarefas.Application.Features.Tasks.Interfaces
{
    public interface ITaskItemRepository
    {
        public Task<TaskItem?> GetByIdAsync(Guid id);
        public Task<List<TaskItem>> GetAllAsync();
        public Task AddAsync(TaskItem taskItem);
        public Task UpdateAsync(TaskItem taskItem);
        public Task DeleteAsync(TaskItem taskItem);
    }
}