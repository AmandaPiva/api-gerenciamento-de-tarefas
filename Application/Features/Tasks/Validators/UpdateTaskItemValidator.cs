using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_gerenciamento_tarefas.Application.Features.Tasks.DTO;
using FluentValidation;

namespace api_gerenciamento_tarefas.Application.Features.Tasks.Validators
{
    public class UpdateTaskItemValidator : AbstractValidator<UpdateTaskItemDto>
    {
        public UpdateTaskItemValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O Id da tarefa é obrigatório.");
            
            RuleFor(x => x.CompletionDate)
                .GreaterThan(DateTime.UtcNow)
                .When(x => x.CompletionDate.HasValue)
                .WithMessage("Data de conclusão deve ser futura");
        }
    }
}