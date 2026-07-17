using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_gerenciamento_tarefas.Application.Features.Projects.Interfaces;
using api_gerenciamento_tarefas.Application.Features.SubTasks.Interfaces;
using api_gerenciamento_tarefas.Application.Features.Tasks.Interfaces;
using api_gerenciamento_tarefas.Application.Features.Users.Interfaces;
using api_gerenciamento_tarefas.Application.Interfaces;
using api_gerenciamento_tarefas.Domain.Entities;

namespace Tests;

internal sealed class FakeUnitOfWork : IUnitOfWork
{
    public FakeUnitOfWork()
    {
        ProjectRepository = new FakeProjectRepository();
        TaskItemRepository = new FakeTaskItemRepository();
        SubtaskRepository = new FakeSubtaskRepository();
        UserRepository = new FakeUserRepository();
    }

    public IProjectRepository ProjectRepository { get; }
    public ITaskItemRepository TaskItemRepository { get; }
    public ISubtaskRepository SubtaskRepository { get; }
    public IUserRepository UserRepository { get; }
    public int SaveChangesCallCount { get; private set; }

    public Task<int> SaveChangesAsync()
    {
        SaveChangesCallCount++;
        return Task.FromResult(1);
    }
}

internal sealed class FakeProjectRepository : IProjectRepository
{
    private readonly List<Project> _projects = new();

    public Task<Project?> GetByIdAsync(Guid id) => Task.FromResult(_projects.FirstOrDefault(p => p.Id == id));
    public Task<List<Project>> GetAllAsync() => Task.FromResult(_projects.ToList());

    public Task AddAsync(Project project)
    {
        _projects.Add(project);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Project project)
    {
        var index = _projects.FindIndex(p => p.Id == project.Id);
        if (index >= 0)
        {
            _projects[index] = project;
        }

        return Task.CompletedTask;
    }

    public Task DeleteAsync(Project project)
    {
        _projects.RemoveAll(p => p.Id == project.Id);
        return Task.CompletedTask;
    }

    public Task<TaskItem?> AddTaskToProjectAsync(Guid projectId, TaskItem task)
    {
        var project = _projects.FirstOrDefault(p => p.Id == projectId);
        if (project == null)
        {
            return Task.FromResult<TaskItem?>(null);
        }

        task.ProjectId = projectId;
        project.Tasks.Add(task);
        return Task.FromResult<TaskItem?>(task);
    }
}

internal sealed class FakeTaskItemRepository : ITaskItemRepository
{
    private readonly List<TaskItem> _tasks = new();

    public Task<TaskItem?> GetByIdAsync(Guid id) => Task.FromResult(_tasks.FirstOrDefault(t => t.Id == id));
    public Task<List<TaskItem>> GetAllAsync() => Task.FromResult(_tasks.ToList());

    public Task AddAsync(TaskItem taskItem)
    {
        _tasks.Add(taskItem);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(TaskItem taskItem)
    {
        var index = _tasks.FindIndex(t => t.Id == taskItem.Id);
        if (index >= 0)
        {
            _tasks[index] = taskItem;
        }

        return Task.CompletedTask;
    }

    public Task DeleteAsync(TaskItem taskItem)
    {
        _tasks.RemoveAll(t => t.Id == taskItem.Id);
        return Task.CompletedTask;
    }
}

internal sealed class FakeSubtaskRepository : ISubtaskRepository
{
    private readonly List<SubTask> _subTasks = new();

    public Task<SubTask?> GetByIdAsync(Guid id) => Task.FromResult(_subTasks.FirstOrDefault(s => s.id == id));
    public Task<List<SubTask>> GetAllAsync() => Task.FromResult(_subTasks.ToList());

    public Task AddAsync(SubTask subTask)
    {
        _subTasks.Add(subTask);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(SubTask subTask)
    {
        var index = _subTasks.FindIndex(s => s.id == subTask.id);
        if (index >= 0)
        {
            _subTasks[index] = subTask;
        }

        return Task.CompletedTask;
    }

    public Task DeleteAsync(SubTask subTask)
    {
        _subTasks.RemoveAll(s => s.id == subTask.id);
        return Task.CompletedTask;
    }
}

internal sealed class FakeUserRepository : IUserRepository
{
    private readonly List<User> _users = new();

    public Task<User?> GetByIdAsync(Guid id) => Task.FromResult(_users.FirstOrDefault(u => u.Id == id));
    public Task<List<User>> GetAllAsync() => Task.FromResult(_users.ToList());

    public Task AddAsync(User user)
    {
        _users.Add(user);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(User user)
    {
        var index = _users.FindIndex(u => u.Id == user.Id);
        if (index >= 0)
        {
            _users[index] = user;
        }

        return Task.CompletedTask;
    }

    public Task DeleteAsync(User user)
    {
        _users.RemoveAll(u => u.Id == user.Id);
        return Task.CompletedTask;
    }
}
