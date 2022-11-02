using FluentValidation;
using HospitalAPI.Dtos.Request;

namespace HospitalAPI.Validations
{
    public class SpecializationRequestValidation:AbstractValidator<SpecializationRequest>
    {
        public SpecializationRequestValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .Matches("^[a-zA-z]*$");
        }
    }
}