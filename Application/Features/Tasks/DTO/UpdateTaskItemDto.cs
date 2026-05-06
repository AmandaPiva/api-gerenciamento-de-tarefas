using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_gerenciamento_tarefas.Application.Features.Tasks.DTO
{
    public class UpdateTaskItemDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? CompletionDate { get; set; }
        public bool Completed { get; set; }
        public bool IsPriority { get; set; }
        
    }
}