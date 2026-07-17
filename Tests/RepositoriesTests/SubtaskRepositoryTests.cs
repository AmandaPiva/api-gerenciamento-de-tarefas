using api_gerenciamento_tarefas.Domain.Entities;
using api_gerenciamento_tarefas.Infraestructure.Repositories;

namespace api_gerenciamento_tarefas.Tests.RepositoriesTests;

public class SubtaskRepositoryTests
{
    [Fact]
    public async Task ShouldPersistSubtasks()
    {
        await using var context = RepositoryTestContextFactory.CreateContext();
        var repository = new SubtaskRepository(context);
        var subtask = new SubTask
        {
            id = Guid.NewGuid(),
            title = "Subtarefa A",
            isCompleted = false,
            TaskId = Guid.NewGuid()
        };

        await repository.AddAsync(subtask);
        await context.SaveChangesAsync();

        var persisted = await repository.GetByIdAsync(subtask.id);
        Assert.NotNull(persisted);
        Assert.Equal("Subtarefa A", persisted!.title);

        persisted.title = "Subtarefa B";
        await repository.UpdateAsync(persisted);
        await context.SaveChangesAsync();

        var updated = await repository.GetByIdAsync(subtask.id);
        Assert.Equal("Subtarefa B", updated!.title);

        await repository.DeleteAsync(updated);
        await context.SaveChangesAsync();

        Assert.Null(await repository.GetByIdAsync(subtask.id));
    }
}
