using FluentValidation;
using HospitalLibrary.Appointments.Model;

namespace HospitalAPI.Validations
{
    public class AppointmentRequestValidation:AbstractValidator<Appointment>
    {
        public AppointmentRequestValidation()
        {
            RuleFor(x => x.Duration)
                .NotEmpty()
                .NotNull()
                .Must(x=> x.From < x.To);
        }
    }
}