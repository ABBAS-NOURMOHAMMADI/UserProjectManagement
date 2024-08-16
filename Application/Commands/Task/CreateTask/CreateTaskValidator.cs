using FluentValidation;

namespace Application.Commands.Task.CreateTask
{
    public class CreateTaskValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskValidator()
        {
            RuleFor(s => s.Name).NotEmpty().WithMessage("نام تسک خالی است");
        }
    }
}
