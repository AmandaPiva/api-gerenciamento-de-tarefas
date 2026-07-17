using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_gerenciamento_tarefas.Application.Features.Tasks.DTO;
using api_gerenciamento_tarefas.Application.Features.Tasks.Validators;

namespace Tests
{
    public class CreateTaskItemValidatorTests
    {
        private readonly CreateTaskItemValidator _validator = new();

        [Fact]
        public void Should_Have_Error_When_Title_Is_Empty()
        {
            var dto = new CreateTaskItemDto
            {
                Title = string.Empty,
                CreationDate = DateTime.UtcNow
            };

            var result = _validator.Validate(dto);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error => error.PropertyName == nameof(CreateTaskItemDto.Title));
        }

        [Fact]
        public void Should_Be_Valid_When_Data_Is_Correct()
        {
            var dto = new CreateTaskItemDto
            {
                Title = "Tarefa de exemplo",
                CreationDate = DateTime.UtcNow.AddDays(-1),
                CompletionDate = DateTime.UtcNow.AddDays(1),
                ProjectId = Guid.NewGuid()
            };

            var result = _validator.Validate(dto);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Should_Have_Error_When_ProjectId_Is_Empty()
        {
            var dto = new CreateTaskItemDto
            {
                Title = "Tarefa de exemplo",
                CreationDate = DateTime.UtcNow,
                ProjectId = Guid.Empty
            };

            var result = _validator.Validate(dto);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error => error.PropertyName == nameof(CreateTaskItemDto.ProjectId));
        }
    }
}