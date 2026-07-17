using api_gerenciamento_tarefas.Domain.Entities;
using api_gerenciamento_tarefas.Infraestructure.Repositories;

namespace api_gerenciamento_tarefas.Tests.RepositoriesTests;

public class TaskRepositoryTests
{
    [Fact]
    public async Task ShouldPersistTaskItems()
    {
        await using var context = RepositoryTestContextFactory.CreateContext();
        var repository = new TaskRepository(context);
        var task = new TaskItem
        {
            Id = Guid.NewGuid(),
            Title = "Tarefa de teste",
            Description = "Descrição da tarefa",
            CreationDate = DateTime.UtcNow,
            ProjectId = Guid.NewGuid()
        };

        await repository.AddAsync(task);
        await context.SaveChangesAsync();

        var persisted = await repository.GetByIdAsync(task.Id);
        Assert.NotNull(persisted);
        Assert.Equal("Tarefa de teste", persisted!.Title);

        persisted.Title = "Tarefa atualizada";
        await repository.UpdateAsync(persisted);
        await context.SaveChangesAsync();

        var updated = await repository.GetByIdAsync(task.Id);
        Assert.Equal("Tarefa atualizada", updated!.Title);

        await repository.DeleteAsync(updated);
        await context.SaveChangesAsync();

        Assert.Null(await repository.GetByIdAsync(task.Id));
    }
}
