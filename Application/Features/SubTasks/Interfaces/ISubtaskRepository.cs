using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_gerenciamento_tarefas.Domain.Entities;

namespace api_gerenciamento_tarefas.Application.Features.SubTasks.Interfaces
{
    public interface ISubtaskRepository
    {
        public Task<SubTask?> GetByIdAsync(Guid id);
        public Task<List<SubTask>> GetAllAsync();
        public Task AddAsync(SubTask subTask);
        public Task UpdateAsync(SubTask subTask);
        public Task DeleteAsync(SubTask subTask);
    }
}