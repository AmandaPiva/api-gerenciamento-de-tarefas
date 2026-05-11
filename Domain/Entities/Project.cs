using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_gerenciamento_tarefas.Domain.Entities
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public DateTime? CompletionDate { get; set; } = null;
        public bool Completed { get; set; } = false;
        public Guid UserId { get; set; } // FK do Usuário
        public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}