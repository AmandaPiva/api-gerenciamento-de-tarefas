using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_gerenciamento_tarefas.Domain.Entities;

namespace api_gerenciamento_tarefas.Application.Features.SubTasks.Interfaces
{
    public interface ISubtaskRepository
    {
        Task<SubTask?> GetByIdAsync(Guid id);
        Task<List<SubTask>> GetAllAsync();
        Task AddAsync(SubTask subTask);
        Task UpdateAsync(SubTask subTask);
        Task DeleteAsync(Guid id);
    }
}