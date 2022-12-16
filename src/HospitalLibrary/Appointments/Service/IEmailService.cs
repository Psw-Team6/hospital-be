using System.Threading.Tasks;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.SharedModel;

namespace HospitalLibrary.Appointments.Service
{
    public interface IEmailService
    {
        public Task<Email> SendEmail(Email email);
        public Task<Email> SendCancelAppointmentEmail(Appointment appointment);
        
        public Task<Email> SendRescheduleAppointmentEmail(Appointment appointment);
    }
}