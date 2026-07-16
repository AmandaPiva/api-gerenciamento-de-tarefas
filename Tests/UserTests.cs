using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_gerenciamento_tarefas.Application.Features.Users.DTO;
using api_gerenciamento_tarefas.Application.Features.Users.Validators;

namespace Tests
{
    public class CreateUserValidatorTests
    {
        private readonly CreateUserValidator _validator = new();

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var dto = new CreateUserDto
            {
                Name = string.Empty,
                Email = "",
                PasswordHash = ""
            };

            var result = _validator.Validate(dto);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error => error.PropertyName == nameof(CreateUserDto.Name));
        }

        [Fact]
        public void Should_Have_Error_When_Email_Is_Empty()
        {
            var dto = new CreateUserDto
            {
                Name = "Usuário de exemplo",
                Email = string.Empty,
                PasswordHash = ""
            };

            var result = _validator.Validate(dto);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error => error.PropertyName == nameof(CreateUserDto.Email));
        }

        [Fact]
        public void Should_Have_Error_When_Password_Is_Empty()
        {
            var dto = new CreateUserDto
            {
                Name = "Usuário de exemplo",
                Email = "usuario@example.com",
                PasswordHash = string.Empty
            };

            var result = _validator.Validate(dto);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error => error.PropertyName == nameof(CreateUserDto.PasswordHash));
        }

        [Fact]
        public void Should_Be_Valid_When_Data_Is_Correct()
        {
            var dto = new CreateUserDto
            {
                Name = "Usuário de exemplo",
                Email = "usuario@example.com",
                PasswordHash = "SenhaSegura123!"
            };

            var result = _validator.Validate(dto);

            Assert.True(result.IsValid);
        }
    }
}