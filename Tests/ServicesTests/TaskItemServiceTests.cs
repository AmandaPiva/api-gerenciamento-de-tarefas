using System;
using System.Threading.Tasks;
using api_gerenciamento_tarefas.Application.Features.Tasks.DTO;
using api_gerenciamento_tarefas.Application.Features.Tasks.Services;
using api_gerenciamento_tarefas.Domain.Entities;

namespace Tests;

public class TaskItemServiceTests
{
    [Fact]
    public async Task GetByIdAsync_Should_Return_Mapped_Task_Response()
    {
        var unitOfWork = new FakeUnitOfWork();
        var service = new TaskItemService(unitOfWork);
        var task = new TaskItem
        {
            Id = Guid.NewGuid(),
            Title = "Estudar",
            Description = "Estudar para prova",
            CreationDate = DateTime.UtcNow.AddDays(-1),
            CompletionDate = DateTime.UtcNow.AddDays(2),
            Completed = false,
            IsPriority = true,
            ProjectId = Guid.NewGuid()
        };
        await unitOfWork.TaskItemRepository.AddAsync(task);

        var result = await service.GetByIdAsync(task.Id);

        Assert.NotNull(result);
        Assert.Equal(task.Title, result!.Title);
        Assert.Equal(task.ProjectId, result.ProjectId);
    }

    [Fact]
    public async Task AddAsync_Should_Add_Task_And_SaveChanges()
    {
        var unitOfWork = new FakeUnitOfWork();
        var service = new TaskItemService(unitOfWork);
        var dto = new CreateTaskItemDto
        {
            Title = "Nova tarefa",
            Description = "Descrição",
            CompletionDate = DateTime.UtcNow.AddDays(3),
            Completed = false,
            IsPriority = false,
            ProjectId = Guid.NewGuid()
        };

        var result = await service.AddAsync(dto);

        Assert.NotNull(result);
        Assert.Equal(dto.Title, result.Title);
        Assert.Equal(1, unitOfWork.SaveChangesCallCount);
        Assert.Single(await unitOfWork.TaskItemRepository.GetAllAsync());
    }

    [Fact]
    public async Task DeleteAsync_Should_Throw_When_Task_Not_Found()
    {
        var unitOfWork = new FakeUnitOfWork();
        var service = new TaskItemService(unitOfWork);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => service.DeleteAsync(Guid.NewGuid()));
    }
}
