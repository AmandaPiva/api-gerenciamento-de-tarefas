using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_gerenciamento_tarefas.Domain.Entities
{
    public class TarefaItem
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
        public DateTime? DataConclusao { get; set; }
        public bool Concluida { get; set; }
        public bool IsPrioritaria { get; set; }
    }
}