using System.Threading.Tasks;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.sharedModel;

namespace HospitalLibrary.Appointments.Service
{
    public interface IEmailService
    {
        public Task SendEmail(Email email);
        public Task SendCancelAppointmentEmail(Appointment appointment);
    }
}