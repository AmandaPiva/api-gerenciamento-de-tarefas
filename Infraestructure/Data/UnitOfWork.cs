using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_gerenciamento_tarefas.Application.Features.Projects.Interfaces;
using api_gerenciamento_tarefas.Application.Features.SubTasks.Interfaces;
using api_gerenciamento_tarefas.Application.Features.Tasks.Interfaces;
using api_gerenciamento_tarefas.Application.Features.Users.Interfaces;
using api_gerenciamento_tarefas.Application.Interfaces;
using api_gerenciamento_tarefas.Data;

namespace api_gerenciamento_tarefas.Infraestructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IProjectRepository ProjectRepository { get; }
        public ITaskItemRepository TaskItemRepository { get; }
        public ISubtaskRepository SubtaskRepository { get; }
        public IUserRepository UserRepository { get; }

        public UnitOfWork(
            AppDbContext context, 
            IProjectRepository projectRepository, 
            ITaskItemRepository taskItemRepository, 
            ISubtaskRepository subtaskRepository, 
            IUserRepository userRepository)
        {
            _context = context;
            ProjectRepository = projectRepository;
            TaskItemRepository = taskItemRepository;
            SubtaskRepository = subtaskRepository;
            UserRepository = userRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}