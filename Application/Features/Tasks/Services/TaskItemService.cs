using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_gerenciamento_tarefas.Application.Features.Tasks.DTO;
using api_gerenciamento_tarefas.Application.Interfaces;
using api_gerenciamento_tarefas.Domain.Entities;

namespace api_gerenciamento_tarefas.Application.Features.Tasks.Services
{
    public class TaskItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TaskItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TaskItemResponseDto?> GetByIdAsync(Guid id)
        {
            var taskItem = await _unitOfWork.TaskItemRepository.GetByIdAsync(id);
            if (taskItem == null)
                throw new Exception("Tarefa não encontrada.");
                
            return new TaskItemResponseDto
            {
                Id = taskItem.Id,
                Title = taskItem.Title,
                Description = taskItem.Description,
                CreationDate = taskItem.CreationDate,
                CompletionDate = taskItem.CompletionDate,
                Completed = taskItem.Completed,
                IsPriority = taskItem.IsPriority,
                ProjectId = taskItem.ProjectId
            };
        }

        public async Task<List<TaskItemResponseDto>> GetAllAsync()
        {
            var taskItems = await _unitOfWork.TaskItemRepository.GetAllAsync();
            return taskItems.Select(t => new TaskItemResponseDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                CreationDate = t.CreationDate,
                CompletionDate = t.CompletionDate,
                Completed = t.Completed,
                IsPriority = t.IsPriority,
                ProjectId = t.ProjectId
            }).ToList();
        }

        public async Task<TaskItem> AddAsync(CreateTaskItemDto dto)
        {
            var taskItem = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                CreationDate = DateTime.UtcNow,
                CompletionDate = dto.CompletionDate,
                Completed = dto.Completed,
                IsPriority = dto.IsPriority,
                ProjectId = dto.ProjectId
            };
           
            await _unitOfWork.TaskItemRepository.AddAsync(taskItem);
            await _unitOfWork.SaveChangesAsync();
            return taskItem;
        }

        public async Task UpdateAsync(UpdateTaskItemDto dto)
        {
            var existingTaskItem = await _unitOfWork.TaskItemRepository.GetByIdAsync(dto.Id);
            if (existingTaskItem == null)
                throw new Exception("Tarefa não encontrada.");
           
            existingTaskItem.Title = dto.Title;
            existingTaskItem.Description = dto.Description;
            existingTaskItem.CompletionDate = dto.CompletionDate;
            existingTaskItem.Completed = dto.Completed;
            existingTaskItem.IsPriority = dto.IsPriority;

            await _unitOfWork.TaskItemRepository.UpdateAsync(existingTaskItem);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var taskItem = await _unitOfWork.TaskItemRepository.GetByIdAsync(id);

            if (taskItem == null)
                throw new Exception("Tarefa não encontrada.");
                
            await _unitOfWork.TaskItemRepository.DeleteAsync(taskItem);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}