using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_gerenciamento_tarefas.Application.Features.SubTasks.Interfaces;
using api_gerenciamento_tarefas.Data;
using api_gerenciamento_tarefas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace api_gerenciamento_tarefas.Infraestructure.Repositories
{
    public class SubtaskRepository : ISubtaskRepository
    {
        private readonly AppDbContext _context;
        public SubtaskRepository(AppDbContext context)
        {
            _context = context;
        }   
        public async Task AddAsync(SubTask subTask)
        {
            await _context.SubTask.AddAsync(subTask);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var subTask = await _context.SubTask.FindAsync(id);
            if (subTask != null)
            {
                _context.SubTask.Remove(subTask);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<SubTask>> GetAllAsync()
        {
            return await _context.SubTask.ToListAsync();
        }

        public async Task<SubTask?> GetByIdAsync(Guid id)
        {
            return await _context.SubTask.FindAsync(id);
        }

        public async Task UpdateAsync(SubTask subTask)
        {
            _context.SubTask.Update(subTask);
            await _context.SaveChangesAsync();
        }
        
    }
}