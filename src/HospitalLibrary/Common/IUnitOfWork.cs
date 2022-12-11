using System;
using System.Threading.Tasks;
using HospitalLibrary.ApplicationUsers.Repository;
using HospitalLibrary.Appointments.Repository;
using HospitalLibrary.BloodConsumptions.Repository;
using HospitalLibrary.BloodUnits.Repository;
using HospitalLibrary.Consiliums.Repository;
using HospitalLibrary.Doctors.Repository;
using HospitalLibrary.EquipmentMovement.Repository;
using HospitalLibrary.Examinations.Repository;
using HospitalLibrary.Feedbacks.Repository;
using HospitalLibrary.Holidays.Repository;
using HospitalLibrary.Medicines.Repository;
using HospitalLibrary.Patients.Repository;
using HospitalLibrary.Prescriptions.Repository;
using HospitalLibrary.Rooms.Repository;
using HospitalLibrary.SharedModel.Repository;
using HospitalLibrary.TreatmentReports.Repository;

namespace HospitalLibrary.Common
{
    public interface IUnitOfWork : IAsyncDisposable,IDisposable
    {
        ISpecializationsRepository SpecializationsRepository { get; }
        IDoctorRepository DoctorRepository { get; }
        IPatientRepository PatientRepository { get; }
        IAppointmentRepository AppointmentRepository { get; }
        IHolidayRepository HolidayRepository { get; }
        IFeedbackRepository FeedbackRepository { get; }
        IWorkingSchueduleRepository WorkingSchueduleRepository { get; }
        IFloorRepository FloorRepository { get; }
        IBuildingRepository BuildingRepository { get; }
        IGRoomRepository GRoomRepository { get; }
        IRoomRepository RoomRepository { get; }
        IApplicationUserRepository UserRepository { get; }
        IBloodUnitRepository BloodUnitRepository { get; }
        IBloodConsumptionRepository BloodConsumptionRepository { get; }
        IPatientAdmissionRepository PatientAdmissionRepository { get; }
        
        IIEquipmentRepository EquipmentRepository { get; }
        ITreatmentReportRepository TreatmentReportRepository { get; }
        IRoomBedRepository RoomBedRepository { get; }
        IEquipmentMovementAppointmentRepository EquipmentMovementAppointmentRepository { get; }
        IMedicinePrescriptionRepository MedicinePrescriptionRepository { get; }

        IAddressRepository AddressRepository { get; }
        IAllergenRepository AllergenRepository { get; }
        IMedicineRepository MedicineRepository { get; }
        ISymptomRepository SymptomRepository { get; }
        IBloodPrescriptionRepository BloodPrescriptionRepository { get; }
        IConsiliumRepository ConsiliumRepository { get; }
        IExaminationRepository ExaminationRepository { get; }
        IExaminationPrescriptionRepository ExaminationPrescriptionRepository { get; }
        T GetRepository<T>() where T : class;
        Task CompleteAsync();
    }
}