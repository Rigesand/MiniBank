using FluentValidation;
using MiniBank.Web.Controllers.Users.Dto;

namespace MiniBank.Web.Controllers.Users.Validators
{
    public class UpdateUserValidator: AbstractValidator<UpdateUserDto>
    {
        public UpdateUserValidator()
        {
            RuleFor(it => it.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(it => it.Login)
                .MaximumLength(20)
                .NotEmpty();
            RuleFor(it => it.Id)
                .NotEmpty();
        }
    }
}