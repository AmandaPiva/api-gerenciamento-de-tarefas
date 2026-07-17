using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_gerenciamento_tarefas.Application.Features.SubTasks.DTO;
using api_gerenciamento_tarefas.Application.Features.SubTasks.Validators;

namespace Tests
{
    public class CreateSubTaskValidatorTests
    {
        private readonly CreateSubtaskValidator _validator = new();

        [Fact]
        public void Should_Have_Error_When_Title_Is_Empty()
        {
            var dto = new CreateSubtaskDto
            {
                Title = string.Empty,
                TaskId = Guid.NewGuid()
            };

            var result = _validator.Validate(dto);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error => error.PropertyName == nameof(CreateSubtaskDto.Title));
        }

        [Fact]
        public void Should_Have_Error_When_TaskId_Is_Empty()
            {
                var dto = new CreateSubtaskDto
                {
                    Title = "Subtarefa de exemplo",
                    TaskId = Guid.Empty
                };

                var result = _validator.Validate(dto);

                Assert.False(result.IsValid);
                Assert.Contains(result.Errors, error => error.PropertyName == nameof(CreateSubtaskDto.TaskId));
            }

        [Fact]
        public void Should_Be_Valid_When_TaskId_Is_Correct()
        {
            var dto = new CreateSubtaskDto
            {
                Title = "Subtarefa de exemplo",
                TaskId = Guid.NewGuid()
            };

            var result = _validator.Validate(dto);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Should_Be_Valid_When_Data_Is_Correct()
        {
            var dto = new CreateSubtaskDto
            {
                Title = "Subtarefa de exemplo",
                TaskId = Guid.NewGuid()
            };

            var result = _validator.Validate(dto);

            Assert.True(result.IsValid);
        }
    }
}