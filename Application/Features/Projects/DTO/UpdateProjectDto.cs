using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_gerenciamento_tarefas.Application.Features.Projects.DTO
{
    public class UpdateProjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public DateTime? CompletionDate { get; set; } = null;
        public bool Completed { get; set; }
    }
}