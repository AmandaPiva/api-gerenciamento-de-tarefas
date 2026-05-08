using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_gerenciamento_tarefas.Application.Features.SubTasks.DTO;
using FluentValidation;

namespace api_gerenciamento_tarefas.Application.Features.SubTasks.Validators
{
    public class UpdateSubtaskValidator : AbstractValidator<UpdateSubtaskDto>
    {
        public UpdateSubtaskValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O Id da subtarefa é obrigatório.");
        }
    }
}