using FluentValidation;
using HospitalAPI.Dtos.Request;

namespace HospitalAPI.Validations
{
    public class DoctorRequestValidation:AbstractValidator<DoctorRequest>
    {
        public DoctorRequestValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull();
        }
    }
}