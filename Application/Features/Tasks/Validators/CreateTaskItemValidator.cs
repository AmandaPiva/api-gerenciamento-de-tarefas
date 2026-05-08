using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using api_gerenciamento_tarefas.Application.Features.Tasks.DTO;
using FluentValidation;

namespace api_gerenciamento_tarefas.Application.Features.Tasks.Validators
{
    public class CreateTaskItemValidator : AbstractValidator<CreateTaskItemDto>
    {
        public CreateTaskItemValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("O título da tarefa é obrigatório.");
            
            RuleFor(x => x.ProjectId)
                .NotEmpty().WithMessage("O Id do projeto é obrigatório.");

            RuleFor(x => x.CreationDate)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Data de criação deve ser no passado ou presente");
        }
    }
}