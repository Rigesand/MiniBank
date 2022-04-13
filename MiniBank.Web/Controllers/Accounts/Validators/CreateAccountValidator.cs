using FluentValidation;
using MiniBank.Web.Controllers.Accounts.Dto;

namespace MiniBank.Web.Controllers.Accounts.Validators
{
    public class CreateAccountValidator: AbstractValidator<CreateAccountDto>
    {
        public CreateAccountValidator()
        {
            RuleFor(it => it.Currency)
                .NotEmpty();
            RuleFor(it => it.Sum)
                .NotEmpty()
                .GreaterThan(0);
            RuleFor(it => it.UserId)
                .NotEmpty();
        }
    }
}