using api_gerenciamento_tarefas.Data;
using Microsoft.EntityFrameworkCore;

namespace api_gerenciamento_tarefas.Tests.RepositoriesTests;

internal static class RepositoryTestContextFactory
{
    public static AppDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }
}
