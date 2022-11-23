using System;
using System.Threading.Tasks;
using HospitalLibrary.ApplicationUsers.Repository;
using HospitalLibrary.Appointments.Repository;
using HospitalLibrary.BloodConsumptions.Repository;
using HospitalLibrary.BloodUnits.Repository;
using HospitalLibrary.Doctors.Repository;
using HospitalLibrary.EquipmentMovement.Repository;
using HospitalLibrary.Feedbacks.Repository;
using HospitalLibrary.Holidays.Repository;
using HospitalLibrary.Patients.Repository;
using HospitalLibrary.Rooms.Repository;
using HospitalLibrary.Settings;
using HospitalLibrary.sharedModel.Repository;
using HospitalLibrary.TreatmentReports.Repository;
using HospitalLibrary.TreatmentReports.Service;

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
        private EquipmentRepository _equipmentRepository;
        private TreatmentReportRepository _treatmentReportRepository;
        private RoomBedRepository _roomBedRepository;
        private EquipmentMovementAppointmentRepository _equipmentMovementAppointmentRepository;
        private AddressRepository _addressRepository;
      

        public IAllergenRepository AllergenRepository =>
            _allergenRepository ??= new AllergenRepository(_hospitalDbContext);
        public IAddressRepository AddressRepository =>
            _addressRepository ??= new AddressRepository(_hospitalDbContext);
        public IRoomBedRepository RoomBedRepository =>
            _roomBedRepository ??= new RoomBedRepository(_hospitalDbContext);


        public ITreatmentReportRepository TreatmentReportRepository =>
            _treatmentReportRepository ??= new TreatmentReportRepository(_hospitalDbContext);
        public IIEquipmentRepository EquipmentRepository =>
            _equipmentRepository ??= new EquipmentRepository(_hospitalDbContext);
        public IPatientAdmissionRepository PatientAdmissionRepository =>
            _patientAdmissionRepository ??= new PatientAdmissionRepository(_hospitalDbContext);
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
        public  IEquipmentMovementAppointmentRepository EquipmentMovementAppointmentRepository => _equipmentMovementAppointmentRepository ??= new EquipmentMovementAppointmentRepository(_hospitalDbContext);



        public IWorkingSchueduleRepository WorkingSchueduleRepository =>
            _workingScheduleRepository ??= new WorkingScheduleRepository(_hospitalDbContext);
        private RoomRepository _roomRepository;
        public IRoomRepository RoomRepository => _roomRepository ??= new RoomRepository(_hospitalDbContext);

        public IApplicationUserRepository UserRepository =>
            _applicationUserRepository ??= new ApplicationUserRepository(_hospitalDbContext);

        

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
    }
}