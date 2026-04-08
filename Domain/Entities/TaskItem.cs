using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_gerenciamento_tarefas.Domain.Entities
{
    public class TaskItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public bool Completed { get; set; }
        public bool IsPriority { get; set; }

        public Guid ProjectId { get; set; }
        public Project? Project { get; set; }

        public List<SubTask> SubTasks { get; set; } = new List<SubTask>();
    }
}