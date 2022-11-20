using System;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Repository;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository;
using HospitalLibrary.Doctors.Repository;
using HospitalLibrary.Feedbacks.Repository;
using HospitalLibrary.Patients.Repository;

namespace HospitalLibrary.Common
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        ISpecializationsRepository SpecializationsRepository { get; }
        IDoctorRepository DoctorRepository { get; }
        IPatientRepository PatientRepository { get; }
        IAppointmentRepository AppointmentRepository { get; }
        IFeedbackRepository FeedbackRepository { get; }
        IWorkingSchueduleRepository WorkingSchueduleRepository { get; }
        IFloorRepository FloorRepository { get; }
        IBuildingRepository BuildingRepository { get; }
        IFloorPlanViewRepository FloorPlanViewRepository { get; }
        Task CompleteAsync();
    }
}