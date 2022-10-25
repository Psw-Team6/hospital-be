using System;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Repository;
using HospitalLibrary.Doctors.Repository;
using HospitalLibrary.Patients.Repository;

namespace HospitalLibrary.Common
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        ISpecializationsRepository SpecializationsRepository { get; }
        IDoctorRepository DoctorRepository { get; }
        IPatientRepository PatientRepository { get; }
        IAppointmentRepository AppointmentRepository { get; }
        
        IWorkingSchueduleRepository WorkingSchueduleRepository { get; }
        Task CompleteAsync();
    }
}