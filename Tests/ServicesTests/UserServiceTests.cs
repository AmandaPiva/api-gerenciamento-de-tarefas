using System;
using System.Threading.Tasks;
using api_gerenciamento_tarefas.Application.Features.Users.DTO;
using api_gerenciamento_tarefas.Application.Features.Users.Services;
using api_gerenciamento_tarefas.Domain.Entities;

namespace Tests;

public class UsersServiceTests
{
    [Fact]
    public async Task GetByIdAsync_Should_Return_Mapped_User_Response()
    {
        var unitOfWork = new FakeUnitOfWork();
        var service = new UsersService(unitOfWork);
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Ana",
            Email = "ana@email.com",
            PasswordHash = "hash"
        };
        await unitOfWork.UserRepository.AddAsync(user);

        var result = await service.GetByIdAsync(user.Id);

        Assert.NotNull(result);
        Assert.Equal(user.Name, result!.Name);
        Assert.Equal(user.Email, result.Email);
    }

    [Fact]
    public async Task AddAsync_Should_Add_User_And_SaveChanges()
    {
        var unitOfWork = new FakeUnitOfWork();
        var service = new UsersService(unitOfWork);
        var dto = new CreateUserDto
        {
            Name = "Bruno",
            Email = "bruno@email.com",
            PasswordHash = "senha"
        };

        var result = await service.AddAsync(dto);

        Assert.NotNull(result);
        Assert.Equal(dto.Name, result.Name);
        Assert.Equal(1, unitOfWork.SaveChangesCallCount);
        Assert.Single(await unitOfWork.UserRepository.GetAllAsync());
    }

    [Fact]
    public async Task UpdateAsync_Should_Throw_When_User_Not_Found()
    {
        var unitOfWork = new FakeUnitOfWork();
        var service = new UsersService(unitOfWork);
        var dto = new UpdateUserDto { Id = Guid.NewGuid(), Name = "Novo nome", Email = "novo@email.com" };

        await Assert.ThrowsAsync<Exception>(() => service.UpdateAsync(dto));
    }
}
