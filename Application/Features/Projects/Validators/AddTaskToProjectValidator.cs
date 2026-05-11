using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using api_gerenciamento_tarefas.Application.Features.Tasks.DTO;
using FluentValidation;

namespace api_gerenciamento_tarefas.Application.Features.Projects.Validators
{
    public class AddTaskToProjectValidator : AbstractValidator<CreateTaskItemDto>
    {
        public AddTaskToProjectValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O ID da tarefa é obrigatório.");
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("O título da tarefa é obrigatório.");
            RuleFor(x => x.CompletionDate)
                .GreaterThanOrEqualTo(DateTime.UtcNow).WithMessage("A data de conclusão deve ser no futuro.");
            RuleFor(x => x.ProjectId)
                .NotEmpty().WithMessage("O ID do projeto é obrigatório.");
        }
    }
}