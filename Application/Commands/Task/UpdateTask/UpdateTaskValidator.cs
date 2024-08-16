using FluentValidation;

namespace Application.Commands.Task.UpdateTask
{
    public class UpdateTaskValidator : AbstractValidator<UpdateTaskCommand>
    {
        public UpdateTaskValidator()
        {
            RuleFor(s => s.Name).NotEmpty().WithMessage("نام تسک خالی است");
        }
    }
}
