using FluentValidation;
using HospitalAPI.Dtos.Request;

namespace HospitalAPI.Validations
{
    public class LoginRequestValidator:AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.PortalUrl)
                .NotEmpty()
                .NotNull();
        }
    }
}