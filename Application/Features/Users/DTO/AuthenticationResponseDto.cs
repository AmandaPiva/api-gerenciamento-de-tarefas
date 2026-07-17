using System;

namespace api_gerenciamento_tarefas.Application.Features.Users.DTO
{
    public class AuthenticationResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
    }
}
