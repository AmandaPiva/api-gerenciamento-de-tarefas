using api_gerenciamento_tarefas.Domain.Entities;
using api_gerenciamento_tarefas.Infraestructure.Repositories;

namespace api_gerenciamento_tarefas.Tests.RepositoriesTests;

public class UserRepositoryTests
{
    [Fact]
    public async Task ShouldPersistAndRetrieveUsers()
    {
        await using var context = RepositoryTestContextFactory.CreateContext();
        var repository = new UserRepository(context);
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Ana",
            Email = "ana@email.com",
            PasswordHash = "hash"
        };

        await repository.AddAsync(user);
        await context.SaveChangesAsync();

        var persisted = await repository.GetByIdAsync(user.Id);
        var allUsers = await repository.GetAllAsync();

        Assert.NotNull(persisted);
        Assert.Equal("Ana", persisted!.Name);
        Assert.Single(allUsers);

        persisted.Name = "Ana Maria";
        await repository.UpdateAsync(persisted);
        await context.SaveChangesAsync();

        var updated = await repository.GetByIdAsync(user.Id);
        Assert.Equal("Ana Maria", updated!.Name);

        await repository.DeleteAsync(updated);
        await context.SaveChangesAsync();

        Assert.Null(await repository.GetByIdAsync(user.Id));
        Assert.Empty(await repository.GetAllAsync());
    }
}
