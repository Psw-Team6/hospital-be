using System;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Repository;
using HospitalLibrary.Doctors.Repository;
using HospitalLibrary.Feedbacks.Repository;
using HospitalLibrary.Patients.Repository;
using HospitalLibrary.Rooms.Repository;
using HospitalLibrary.Settings;

namespace HospitalLibrary.Common
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly HospitalDbContext _hospitalDbContext;
        private SpecializationsRepository _specializationsRepository;
        private DoctorRepository _doctorRepository;
        private PatientRepository _patientRepository;
        private AppointmentRepository _appointmentRepository;
        private WorkingScheduleRepository _workingScheduleRepository;
        private FeedbackRepository _feedbackRepository;
        private BuildingRepository _buildingRepository;
        private FloorRepository _floorRepository;
        private FloorPlanViewRepository _floorPlanViewRepository;
        private GRoomRepository _gRoomRepository;

        public IBuildingRepository BuildingRepository =>
            _buildingRepository ??= new BuildingRepository(_hospitalDbContext);
        public IFloorRepository FloorRepository =>
            _floorRepository ??= new FloorRepository(_hospitalDbContext);
        public IFloorPlanViewRepository FloorPlanViewRepository =>
            _floorPlanViewRepository ??= new FloorPlanViewRepository(_hospitalDbContext);

        public IGRoomRepository GRoomRepository =>
            _gRoomRepository ??= new GRoomRepository(_hospitalDbContext);

        public IFeedbackRepository FeedbackRepository => _feedbackRepository ??= new FeedbackRepository(_hospitalDbContext);
        public  IPatientRepository PatientRepository => _patientRepository ??= new PatientRepository(_hospitalDbContext);
        public  IAppointmentRepository AppointmentRepository => _appointmentRepository ??= new AppointmentRepository(_hospitalDbContext);

        public IWorkingSchueduleRepository WorkingSchueduleRepository =>
            _workingScheduleRepository ??= new WorkingScheduleRepository(_hospitalDbContext);
        private RoomRepository _roomRepository;
        public IRoomRepository RoomRepository => _roomRepository ??= new RoomRepository(_hospitalDbContext);

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