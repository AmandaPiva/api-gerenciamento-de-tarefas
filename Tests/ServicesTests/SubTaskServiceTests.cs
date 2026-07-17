using System;
using System.Threading.Tasks;
using api_gerenciamento_tarefas.Application.Features.SubTasks.DTO;
using api_gerenciamento_tarefas.Application.Features.SubTasks.Services;
using api_gerenciamento_tarefas.Domain.Entities;

namespace Tests;

public class SubTaskServiceTests
{
    [Fact]
    public async Task GetByIdAsync_Should_Return_Mapped_Subtask_Response()
    {
        var unitOfWork = new FakeUnitOfWork();
        var service = new SubTaskService(unitOfWork);
        var subTask = new SubTask
        {
            id = Guid.NewGuid(),
            title = "Subtarefa 1",
            isCompleted = false,
            TaskId = Guid.NewGuid()
        };
        await unitOfWork.SubtaskRepository.AddAsync(subTask);

        var result = await service.GetByIdAsync(subTask.id);

        Assert.NotNull(result);
        Assert.Equal(subTask.title, result!.Title);
        Assert.Equal(subTask.isCompleted, result.IsCompleted);
    }

    [Fact]
    public async Task AddAsync_Should_Add_Subtask_And_SaveChanges()
    {
        var unitOfWork = new FakeUnitOfWork();
        var service = new SubTaskService(unitOfWork);
        var dto = new CreateSubtaskDto
        {
            Title = "Nova subtarefa",
            IsCompleted = false,
            TaskId = Guid.NewGuid()
        };

        var result = await service.AddAsync(dto);

        Assert.NotNull(result);
        Assert.Equal(dto.Title, result.title);
        Assert.Equal(1, unitOfWork.SaveChangesCallCount);
        Assert.Single(await unitOfWork.SubtaskRepository.GetAllAsync());
    }

    [Fact]
    public async Task UpdateAsync_Should_Throw_When_Subtask_Not_Found()
    {
        var unitOfWork = new FakeUnitOfWork();
        var service = new SubTaskService(unitOfWork);
        var dto = new UpdateSubtaskDto { Id = Guid.NewGuid(), Title = "Atualizar", IsCompleted = true };

        await Assert.ThrowsAsync<Exception>(() => service.UpdateAsync(dto));
    }
}
