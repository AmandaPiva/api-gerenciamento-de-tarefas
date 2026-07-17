using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using api_gerenciamento_tarefas.Application.Features.SubTasks.DTO;
using FluentValidation;

namespace api_gerenciamento_tarefas.Application.Features.SubTasks.Validators
{
    public class CreateSubtaskValidator : AbstractValidator<CreateSubtaskDto>
    {
        public CreateSubtaskValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("O título da subtarefa é obrigatório.");
            
            RuleFor(x => x.TaskId)
                .NotEmpty().WithMessage("O Id da tarefa é obrigatório.");
        }
    }
}