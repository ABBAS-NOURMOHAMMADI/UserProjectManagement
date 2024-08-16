using FluentValidation;

namespace Application.Commands.Project.CreateProject
{
    public class CreateProjectValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectValidator()
        {
            RuleFor(s => s.Name).NotEmpty().WithMessage("نام پروژه خالی است");
        }
    }
}
