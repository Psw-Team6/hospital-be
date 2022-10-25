using System;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Repository;
using HospitalLibrary.Doctors.Repository;
using HospitalLibrary.Patients.Repository;
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
        public  IPatientRepository PatientRepository => _patientRepository ??= new PatientRepository(_hospitalDbContext);
        public  IAppointmentRepository AppointmentRepository => _appointmentRepository ??= new AppointmentRepository(_hospitalDbContext);

        public ISpecializationsRepository SpecializationsRepository=> _specializationsRepository ??= new SpecializationsRepository(_hospitalDbContext);
        public IDoctorRepository DoctorRepository=> _doctorRepository ??= new DoctorRepository(_hospitalDbContext);
        private bool _disposed;
        public UnitOfWork(HospitalDbContext hospitalDbContext)
        {
            _hospitalDbContext = hospitalDbContext ?? throw new ArgumentNullException(nameof(hospitalDbContext));
        }
        public async Task CompleteAsync()=> await _hospitalDbContext.SaveChangesAsync();
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