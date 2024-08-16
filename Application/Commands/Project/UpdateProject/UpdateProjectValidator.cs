using FluentValidation;

namespace Application.Commands.Project.UpdateProject
{
    public class UpdateProjectValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectValidator()
        {
            RuleFor(s => s.Name).NotEmpty().WithMessage("نام پروژه خالی است");
        }
    }
}
