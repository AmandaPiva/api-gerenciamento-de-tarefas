using api_gerenciamento_tarefas.Domain.Entities;
using api_gerenciamento_tarefas.Infraestructure.Repositories;

namespace api_gerenciamento_tarefas.Tests.RepositoriesTests;

public class ProjectRepositoryTests
{
    [Fact]
    public async Task ShouldPersistProjectsAndAssociateTasks()
    {
        await using var context = RepositoryTestContextFactory.CreateContext();
        var repository = new ProjectRepository(context);
        var project = new Project
        {
            Id = Guid.NewGuid(),
            Name = "Projeto Alpha",
            Description = "Descrição do projeto",
            CreationDate = DateTime.UtcNow,
            UserId = Guid.NewGuid()
        };

        await repository.AddAsync(project);
        await context.SaveChangesAsync();

        var persisted = await repository.GetByIdAsync(project.Id);
        Assert.NotNull(persisted);
        Assert.Equal("Projeto Alpha", persisted!.Name);

        var task = new TaskItem
        {
            Id = Guid.NewGuid(),
            Title = "Tarefa 1",
            Description = "Descrição da tarefa",
            CreationDate = DateTime.UtcNow,
            ProjectId = project.Id
        };

        var createdTask = await repository.AddTaskToProjectAsync(project.Id, task);
        Assert.NotNull(createdTask);

        var projectWithTasks = await repository.GetByIdAsync(project.Id);
        Assert.NotEmpty(projectWithTasks!.Tasks);

        projectWithTasks.Name = "Projeto Atualizado";
        await repository.UpdateAsync(projectWithTasks);
        await context.SaveChangesAsync();

        var updated = await repository.GetByIdAsync(project.Id);
        Assert.Equal("Projeto Atualizado", updated!.Name);

        await repository.DeleteAsync(updated);
        await context.SaveChangesAsync();

        Assert.Null(await repository.GetByIdAsync(project.Id));
    }
}
