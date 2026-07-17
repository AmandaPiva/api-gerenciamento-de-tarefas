using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using api_gerenciamento_tarefas.Application.Features.Projects.DTO;
using FluentValidation;

namespace api_gerenciamento_tarefas.Application.Features.Projects.Validators
{
    public class CreateProjectValidator : AbstractValidator<CreateProjectDto>
    {
        public CreateProjectValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome do projeto é obrigatório.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("O id do usuário é obrigatório.");
            
            RuleFor(x => x.CompletionDate)
                .GreaterThan(x => DateTime.UtcNow)
                .When(x => x.CompletionDate.HasValue)
                .WithMessage("Data de conclusão deve ser futura");

            RuleFor(x => x.CreationDate)
                .LessThanOrEqualTo(x => DateTime.UtcNow)
                .WithMessage("Data de criação deve ser no passado ou presente");
        }
    }
}