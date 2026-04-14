using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_gerenciamento_tarefas.Application.Features.Projects.DTO
{
    public class ProjectResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public bool Completed { get; set; }
    }
}