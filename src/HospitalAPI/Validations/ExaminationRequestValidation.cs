using FluentValidation;
using HospitalAPI.Dtos.Request;

namespace HospitalAPI.Validations
{
    public class ExaminationRequestValidation:AbstractValidator<ExaminationRequest>
    {
        public ExaminationRequestValidation()
        {
            RuleFor(x => x.Symptoms)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.Prescriptions)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.IdApp)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.Anamnesis)
                .NotEmpty()
                .NotNull();
        }
    }
}