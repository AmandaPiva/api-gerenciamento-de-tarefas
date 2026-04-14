using Microsoft.EntityFrameworkCore;
using api_gerenciamento_tarefas.Data;
using api_gerenciamento_tarefas.Application.Interfaces;
using api_gerenciamento_tarefas.Infraestructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddOpenApi();

var app = builder.Build();

app.Run();


