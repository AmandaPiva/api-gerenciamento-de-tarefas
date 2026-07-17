using System;
using System.Threading.Tasks;
using api_gerenciamento_tarefas.Application.Features.Projects.DTO;
using api_gerenciamento_tarefas.Application.Features.Projects.Services;
using api_gerenciamento_tarefas.Application.Features.Tasks.DTO;
using api_gerenciamento_tarefas.Domain.Entities;

namespace Tests;

public class ProjectServiceTests
{
    [Fact]
    public async Task GetByIdAsync_Should_Return_Mapped_Project_Response()
    {
        var unitOfWork = new FakeUnitOfWork();
        var service = new ProjectService(unitOfWork);
        var project = new Project
        {
            Id = Guid.NewGuid(),
            Name = "Projeto A",
            Description = "Descrição",
            CreationDate = DateTime.UtcNow.AddDays(-1),
            CompletionDate = DateTime.UtcNow.AddDays(5),
            Completed = false,
            UserId = Guid.NewGuid()
        };
        await unitOfWork.ProjectRepository.AddAsync(project);

        var result = await service.GetByIdAsync(project.Id);

        Assert.NotNull(result);
        Assert.Equal(project.Name, result!.Name);
        Assert.Equal(project.UserId, result.UserId);
    }

    [Fact]
    public async Task AddAsync_Should_Add_Project_And_SaveChanges()
    {
        var unitOfWork = new FakeUnitOfWork();
        var service = new ProjectService(unitOfWork);
        var dto = new CreateProjectDto
        {
            Name = "Novo projeto",
            Description = "Nova descrição",
            CompletionDate = DateTime.UtcNow.AddDays(1),
            Completed = false,
            UserId = Guid.NewGuid()
        };

        var result = await service.AddAsync(dto);

        Assert.NotNull(result);
        Assert.Equal(dto.Name, result.Name);
        Assert.Equal(1, unitOfWork.SaveChangesCallCount);
        Assert.Single(await unitOfWork.ProjectRepository.GetAllAsync());
    }

    [Fact]
    public async Task AddTaskToProjectAsync_Should_Add_Task_To_Project()
    {
        var unitOfWork = new FakeUnitOfWork();
        var service = new ProjectService(unitOfWork);
        var project = new Project { Id = Guid.NewGuid(), Name = "Projeto", UserId = Guid.NewGuid() };
        await unitOfWork.ProjectRepository.AddAsync(project);

        var taskDto = new CreateTaskItemDto
        {
            Title = "Tarefa",
            Description = "Descrição da tarefa",
            Completed = false,
            IsPriority = true,
            ProjectId = project.Id
        };

        var result = await service.AddTaskToProjectAsync(project.Id, taskDto);

        Assert.NotNull(result);
        Assert.Equal(taskDto.Title, result.Title);
        Assert.Equal(project.Id, result.ProjectId);
    }
}
