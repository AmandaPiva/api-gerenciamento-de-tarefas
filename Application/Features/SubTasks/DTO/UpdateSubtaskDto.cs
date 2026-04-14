using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_gerenciamento_tarefas.Application.Features.SubTasks.DTO
{
    public class UpdateSubtaskDto
    {
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }

    }
}