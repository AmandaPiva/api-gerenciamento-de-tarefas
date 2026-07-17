using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_gerenciamento_tarefas.Application.Features.SubTasks.DTO;

namespace api_gerenciamento_tarefas.Application.Features.Tasks.DTO
{
    public class CreateTaskItemDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public DateTime? CompletionDate { get; set; } = null;
        public bool Completed { get; set; }
        public bool IsPriority { get; set; }
        public Guid ProjectId { get; set; }  // FK do Projeto
    }
}