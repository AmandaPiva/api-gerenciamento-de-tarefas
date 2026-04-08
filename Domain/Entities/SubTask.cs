using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_gerenciamento_tarefas.Domain.Entities
{
    public class SubTask
    {
        public Guid id { get; set; }
        public string title { get; set; } = string.Empty;
        public bool isCompleted { get; set; }

        public Guid TaskId { get; set; }
        public TaskItem? Task { get; set; }
    }
}