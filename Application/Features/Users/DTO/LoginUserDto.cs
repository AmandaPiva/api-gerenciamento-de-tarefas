using System;

namespace api_gerenciamento_tarefas.Application.Features.Users.DTO
{
    public class LoginUserDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
