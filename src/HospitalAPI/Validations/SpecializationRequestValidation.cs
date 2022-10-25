using FluentValidation;
using HospitalAPI.Dtos;

namespace HospitalAPI.Validations
{
    public class SpecializationRequestValidation:AbstractValidator<SpecializationDto>
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