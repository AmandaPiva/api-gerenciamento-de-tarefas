using api_gerenciamento_tarefas.Application.Features.Projects.DTO;
using api_gerenciamento_tarefas.Application.Features.Projects.Validators;

namespace Tests;

public class CreateProjectValidatorTests
{
    private readonly CreateProjectValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        var dto = new CreateProjectDto
        {
            Name = string.Empty,
            CreationDate = DateTime.UtcNow
        };

        var result = _validator.Validate(dto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.PropertyName == nameof(CreateProjectDto.Name));
    }

    [Fact]
    public void Should_Be_Valid_When_Data_Is_Correct()
    {
        var dto = new CreateProjectDto
        {
            Name = "Projeto de exemplo",
            CreationDate = DateTime.UtcNow.AddDays(-1),
            CompletionDate = DateTime.UtcNow.AddDays(1),
            UserId = Guid.NewGuid()
        };

        var result = _validator.Validate(dto);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Should_Have_Error_When_UserId_Is_Empty()
    {
        var dto = new CreateProjectDto
        {
            Name = "Projeto de exemplo",
            CreationDate = DateTime.UtcNow,
            UserId = Guid.Empty
        };

        var result = _validator.Validate(dto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.PropertyName == nameof(CreateProjectDto.UserId));
    }
}
