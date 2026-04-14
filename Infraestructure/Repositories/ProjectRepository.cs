using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_gerenciamento_tarefas.Application.Features.Projects.Interfaces;
using api_gerenciamento_tarefas.Data;
using api_gerenciamento_tarefas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace api_gerenciamento_tarefas.Infraestructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;
        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Project project)
        {
            await _context.Project.AddAsync(project);
        }

        // public async Task<TaskItem?> AddTaskToProjectAsync(Guid projectId, TaskItem task)
        // {
        //     var project = await _context.Project.Include(p => p.Tasks).FirstOrDefaultAsync(p => p.Id == projectId);
        //     if (project != null)
        //     {
        //         project.Tasks.Add(task);
        //         await _context.SaveChangesAsync();
        //         return task;
        //     }
        //     return null;
        // }

        public async Task DeleteAsync(Project project)
        {
           _context.Project.Remove(project);            
        }

        public async Task<List<Project>> GetAllAsync()
        {
            return await _context.Project.ToListAsync();
        }

        public async Task<Project?> GetByIdAsync(Guid id)
        {
            return await _context.Project.FindAsync(id);
        }

        public async Task UpdateAsync(Project project)
        {
            _context.Project.Update(project);
        }
    }
}