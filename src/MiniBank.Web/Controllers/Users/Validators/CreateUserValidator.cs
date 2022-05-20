using FluentValidation;
using MiniBank.Core.Domains.Users.Services;
using MiniBank.Web.Controllers.Users.Dto;

namespace MiniBank.Web.Controllers.Users.Validators
{
    public class CreateUserValidator:AbstractValidator<CreateUserDto>
    {
        public CreateUserValidator(IUserService userService)
        {
            RuleFor(it => it.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(it => it.Login)
                .MaximumLength(20)
                .NotEmpty();
            RuleFor(x => x)
                .Must((user) => !userService.IsExist(user.Login).Result)
                .WithMessage("Email: Такой login уже существует");
        }
    }
}