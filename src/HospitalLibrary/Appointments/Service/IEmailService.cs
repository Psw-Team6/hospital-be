using System.Threading.Tasks;
using HospitalLibrary.sharedModel;

namespace HospitalLibrary.Appointments.Service
{
    public interface IEmailService
    {
        public Task SendEmail(Email email);
    }
}