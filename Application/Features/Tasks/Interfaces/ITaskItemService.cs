using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api_gerenciamento_tarefas.Application.Features.Tasks.DTO;

namespace api_gerenciamento_tarefas.Application.Features.Tasks.Interfaces
{
    public interface ITaskItemService
    {
        Task<TaskItemResponseDto?> GetByIdAsync(Guid id);
        Task<List<TaskItemResponseDto>> GetAllAsync();
        Task<TaskItemResponseDto> AddAsync(CreateTaskItemDto dto);
        Task UpdateAsync(UpdateTaskItemDto dto);
        Task DeleteAsync(Guid id);
    }
}
