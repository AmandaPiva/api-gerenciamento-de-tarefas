using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_gerenciamento_tarefas.Application.Features.SubTasks.DTO;
using api_gerenciamento_tarefas.Application.Interfaces;
using api_gerenciamento_tarefas.Domain.Entities;

namespace api_gerenciamento_tarefas.Application.Features.SubTasks.Services
{
    public class SubTaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SubTaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }   

        public async Task<SubtaskResponseDto?> GetByIdAsync(Guid id)
        {
            var subTask = await _unitOfWork.SubtaskRepository.GetByIdAsync(id);
            if (subTask == null)
                throw new Exception("Subtarefa não encontrada.");
                
            return new SubtaskResponseDto
            {
                Id = subTask.id,
                Title = subTask.title,
                IsCompleted = subTask.isCompleted,
            };
        }

        public async Task<List<SubtaskResponseDto>> GetAllAsync()
        {
            var subTasks = await _unitOfWork.SubtaskRepository.GetAllAsync();
            return subTasks.Select(st => new SubtaskResponseDto
            {
                Id = st.id,
                Title = st.title,
                IsCompleted = st.isCompleted,
            }).ToList();
        }

        public async Task<SubTask> AddAsync(CreateSubtaskDto dto)
        {
            var subTask = new SubTask
            {
                id = Guid.NewGuid(),
                title = dto.Title,
                isCompleted = dto.IsCompleted,
                TaskId = dto.TaskId
            };
          
            await _unitOfWork.SubtaskRepository.AddAsync(subTask);
            await _unitOfWork.SaveChangesAsync();
            return subTask;
        }

        public async Task UpdateAsync(UpdateSubtaskDto dto)
        {
            var existingSubTask = await _unitOfWork.SubtaskRepository.GetByIdAsync(dto.Id);
            if (existingSubTask == null)
                throw new Exception("Subtarefa não encontrada.");

            existingSubTask.title = dto.Title;
            existingSubTask.isCompleted = dto.IsCompleted;

            await _unitOfWork.SubtaskRepository.UpdateAsync(existingSubTask);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var subTask = await _unitOfWork.SubtaskRepository.GetByIdAsync(id);
            if (subTask == null)
                throw new Exception("Subtarefa não encontrada.");
           
            await _unitOfWork.SubtaskRepository.DeleteAsync(subTask);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}