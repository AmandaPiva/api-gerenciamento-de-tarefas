using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_gerenciamento_tarefas.Application.Features.Tasks.Interfaces;
using api_gerenciamento_tarefas.Data;
using api_gerenciamento_tarefas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace api_gerenciamento_tarefas.Infraestructure.Repositories
{
    public class TaskRepository : ITaskItemRepository
    {

        private readonly AppDbContext _context;
        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TaskItem taskItem)
        {
            await _context.TaskItem.AddAsync(taskItem);
        }

        // public async Task<SubTask?> AddSubTaskToTaskAsync(Guid taskId, SubTask subTask)
        // {
        //     var task = await _context.TaskItem.FindAsync(taskId);
        //     task.SubTasks.Add(subTask);
        //     return subTask;
        // }

        public async Task DeleteAsync(TaskItem taskItem)
        {
           _context.TaskItem.Remove(taskItem);
        }

        public async Task<List<TaskItem>> GetAllAsync()
        {
            return await _context.TaskItem.ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(Guid id)
        {
            return await _context.TaskItem.FindAsync(id);
        }

        public async Task UpdateAsync(TaskItem taskItem)
        {
            _context.TaskItem.Update(taskItem);
        }
        
    }
}