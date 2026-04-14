using Microsoft.EntityFrameworkCore;
using api_gerenciamento_tarefas.Data;
using api_gerenciamento_tarefas.Application.Interfaces;
using api_gerenciamento_tarefas.Infraestructure.Data;
using api_gerenciamento_tarefas.Application.Features.Projects.Interfaces;
using api_gerenciamento_tarefas.Infraestructure.Repositories;
using api_gerenciamento_tarefas.Application.Features.Tasks.Interfaces;
using api_gerenciamento_tarefas.Application.Features.SubTasks.Interfaces;
using api_gerenciamento_tarefas.Application.Features.Projects.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Repositórios
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ITaskItemRepository, TaskRepository>();
builder.Services.AddScoped<ISubtaskRepository, SubtaskRepository>();

// Services
builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<TaskItemService>();
builder.Services.AddScoped<SubtaskService>();
builder.Services.AddScoped<UserService>();

builder.Services.AddOpenApi();

var app = builder.Build();

app.Run();

internal class UserService
{
}

internal class SubtaskService
{
}

internal class TaskItemService
{
}