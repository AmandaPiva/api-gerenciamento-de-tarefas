using Microsoft.EntityFrameworkCore;
using api_gerenciamento_tarefas.Data;
using api_gerenciamento_tarefas.Application.Interfaces;
using api_gerenciamento_tarefas.Infraestructure.Data;
using api_gerenciamento_tarefas.Application.Features.Projects.Interfaces;
using api_gerenciamento_tarefas.Infraestructure.Repositories;
using api_gerenciamento_tarefas.Application.Features.Tasks.Interfaces;
using api_gerenciamento_tarefas.Application.Features.SubTasks.Interfaces;
using api_gerenciamento_tarefas.Application.Features.Projects.Services;
using api_gerenciamento_tarefas.Application.Features.Users.Interfaces;
using FluentValidation.AspNetCore;
using api_gerenciamento_tarefas.Application.Features.Users.Services;
using api_gerenciamento_tarefas.Application.Features.Tasks.Services;
using api_gerenciamento_tarefas.Application.Features.SubTasks.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddFluentValidationAutoValidation();

// configurações do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Repositórios
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ITaskItemRepository, TaskRepository>();
builder.Services.AddScoped<ISubtaskRepository, SubtaskRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Services
builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<ITaskItemService, TaskItemService>();
builder.Services.AddScoped<SubTaskService>();
builder.Services.AddScoped<UsersService>();

// Controller
builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();