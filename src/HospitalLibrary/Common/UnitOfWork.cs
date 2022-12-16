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
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Rooms.Repository;
using HospitalLibrary.Settings;
using HospitalLibrary.SharedModel.Repository;
using HospitalLibrary.TreatmentReports.Repository;

namespace HospitalLibrary.Common
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly HospitalDbContext _hospitalDbContext;
        private AllergenRepository _allergenRepository;
        private SpecializationsRepository _specializationsRepository;
        private DoctorRepository _doctorRepository;
        private PatientRepository _patientRepository;
        private AppointmentRepository _appointmentRepository;
        private HolidayRepository _holidayRepository;
        private WorkingScheduleRepository _workingScheduleRepository;
        private FeedbackRepository _feedbackRepository;
        private BuildingRepository _buildingRepository;
        private FloorRepository _floorRepository;
        private GRoomRepository _gRoomRepository;
        private ApplicationUserRepository _applicationUserRepository;
        private BloodUnitRepository _bloodUnitRepository;
        private BloodConsumptionRepository _bloodConsumptionRepository;
        private PatientAdmissionRepository _patientAdmissionRepository;
        private MaliciousPatientRepository _maliciousPatientRepository;
        private EquipmentRepository _equipmentRepository;
        private TreatmentReportRepository _treatmentReportRepository;
        private RoomBedRepository _roomBedRepository;
        private EquipmentMovementAppointmentRepository _equipmentMovementAppointmentRepository;
        private AddressRepository _addressRepository;
        private MedicineRepository _medicineRepository;
        private BloodPrescriptionRepository _bloodPrescriptionRepository;
        private MedicinePrescriptionRepository _medicinePrescriptionRepository;
        private SymptomRepository _symptomRepository;
        private ConsiliumRepository _consiliumRepository;
        private ExaminationRepository _examinationRepository;
        private ExaminationPrescriptionRepository _examinationPrescriptionRepository;
        private RoomRepository _roomRepository;
        private IRoomMergingRepository _roomMergingRepository;
        private IRoomSplitingRepository _roomSplitingRepository;
        
        
        public IAllergenRepository AllergenRepository =>
            _allergenRepository ??= new AllergenRepository(_hospitalDbContext);
        public IConsiliumRepository ConsiliumRepository =>
            _consiliumRepository ??= new ConsiliumRepository(_hospitalDbContext);

        public IMedicinePrescriptionRepository MedicinePrescriptionRepository =>
            _medicinePrescriptionRepository ??= new MedicinePrescriptionRepository(_hospitalDbContext);
        public IAddressRepository AddressRepository =>
            _addressRepository ??= new AddressRepository(_hospitalDbContext);
        public IRoomBedRepository RoomBedRepository =>
            _roomBedRepository ??= new RoomBedRepository(_hospitalDbContext);

        public IMedicineRepository MedicineRepository =>
            _medicineRepository ??= new MedicineRepository(_hospitalDbContext);

        public ISymptomRepository SymptomRepository =>
            _symptomRepository ??= new SymptomRepository(_hospitalDbContext);

        public IBloodPrescriptionRepository BloodPrescriptionRepository =>
            _bloodPrescriptionRepository ??= new BloodPrescriptionRepository(_hospitalDbContext);

        public IExaminationRepository ExaminationRepository =>
            _examinationRepository ??= new ExaminationRepository(_hospitalDbContext);

        public IExaminationPrescriptionRepository ExaminationPrescriptionRepository =>
            _examinationPrescriptionRepository ??= new ExaminationPrescriptionRepository(_hospitalDbContext);

        public ITreatmentReportRepository TreatmentReportRepository =>
            _treatmentReportRepository ??= new TreatmentReportRepository(_hospitalDbContext);
        public IIEquipmentRepository EquipmentRepository =>
            _equipmentRepository ??= new EquipmentRepository(_hospitalDbContext);
        public IPatientAdmissionRepository PatientAdmissionRepository =>
            _patientAdmissionRepository ??= new PatientAdmissionRepository(_hospitalDbContext);
        public IMaliciousPatientRepository MaliciousPatientRepository =>
            _maliciousPatientRepository ??= new MaliciousPatientRepository(_hospitalDbContext);
        public IBloodConsumptionRepository BloodConsumptionRepository =>
            _bloodConsumptionRepository ??= new BloodConsumptionRepository(_hospitalDbContext);
        public IBloodUnitRepository BloodUnitRepository =>
            _bloodUnitRepository ??= new BloodUnitRepository(_hospitalDbContext);
        public IBuildingRepository BuildingRepository =>
            _buildingRepository ??= new BuildingRepository(_hospitalDbContext);
        public IFloorRepository FloorRepository =>
            _floorRepository ??= new FloorRepository(_hospitalDbContext);

        public IGRoomRepository GRoomRepository =>
            _gRoomRepository ??= new GRoomRepository(_hospitalDbContext);

        public IHolidayRepository HolidayRepository => _holidayRepository ??= new HolidayRepository(_hospitalDbContext);
        public IFeedbackRepository FeedbackRepository => _feedbackRepository ??= new FeedbackRepository(_hospitalDbContext);
        public  IPatientRepository PatientRepository => _patientRepository ??= new PatientRepository(_hospitalDbContext);
        public  IAppointmentRepository AppointmentRepository => _appointmentRepository ??= new AppointmentRepository(_hospitalDbContext);
        public  IEquipmentMovementAppointmentRepository EquipmentMovementAppointmentRepository => 
            _equipmentMovementAppointmentRepository ??= new EquipmentMovementAppointmentRepository(_hospitalDbContext);
        
        public IWorkingSchueduleRepository WorkingSchueduleRepository =>
            _workingScheduleRepository ??= new WorkingScheduleRepository(_hospitalDbContext);
 
        public IRoomRepository RoomRepository => _roomRepository ??= new RoomRepository(_hospitalDbContext);

        public IApplicationUserRepository UserRepository =>
            _applicationUserRepository ??= new ApplicationUserRepository(_hospitalDbContext);
        
        public IRoomMergingRepository RoomMergingRepository =>
            _roomMergingRepository ??= new RoomMerginRepository(_hospitalDbContext);
        public IRoomSplitingRepository RoomSplitingRepository =>
            _roomSplitingRepository ??= new RoomSplitingRepository(_hospitalDbContext);

        public ISpecializationsRepository SpecializationsRepository=> _specializationsRepository ??= new SpecializationsRepository(_hospitalDbContext);
        public IDoctorRepository DoctorRepository=> _doctorRepository ??= new DoctorRepository(_hospitalDbContext);
        private bool _disposed;
        public UnitOfWork(HospitalDbContext hospitalDbContext)
        {
            _hospitalDbContext = hospitalDbContext ?? throw new ArgumentNullException(nameof(hospitalDbContext));
        }
        public async Task CompleteAsync()=> await _hospitalDbContext.SaveChangesAsync();
        
        public T GetRepository<T>() where T : class
        {          
            var result = (T)Activator.CreateInstance(typeof(T), _hospitalDbContext);
            return result;
        }
        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }

        private async ValueTask DisposeAsync(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)         
                {
                    await _hospitalDbContext.DisposeAsync();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            if (_disposed) return;
            _hospitalDbContext?.Dispose();
            GC.SuppressFinalize(this);
            _disposed = true;
        }
    }
}