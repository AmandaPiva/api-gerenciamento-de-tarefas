using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_gerenciamento_tarefas.Application.Features.Projects.DTO;
using FluentValidation;

namespace api_gerenciamento_tarefas.Application.Features.Projects.Validators
{
    public class UpdateProjectValidator : AbstractValidator<UpdateProjectDto>
    {
        public UpdateProjectValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O Id do projeto é obrigatório.");
                
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome do projeto é obrigatório.");
            
            RuleFor(x => x.CompletionDate)
                .GreaterThan(DateTime.UtcNow)
                .When(x => x.CompletionDate.HasValue)
                .WithMessage("Data de conclusão deve ser futura");
        }
    }
}