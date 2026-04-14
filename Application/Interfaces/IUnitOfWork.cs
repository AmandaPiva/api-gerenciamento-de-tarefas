using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_gerenciamento_tarefas.Application.Features.Projects.Interfaces;
using api_gerenciamento_tarefas.Application.Features.SubTasks.Interfaces;
using api_gerenciamento_tarefas.Application.Features.Tasks.Interfaces;
using api_gerenciamento_tarefas.Application.Features.Users.Interfaces;
namespace api_gerenciamento_tarefas.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IProjectRepository ProjectRepository { get; }
        ITaskItemRepository TaskItemRepository { get; }
        ISubtaskRepository SubtaskRepository { get; }
        IUserRepository UserRepository { get; }
        Task<int> SaveChangesAsync();
    }
}