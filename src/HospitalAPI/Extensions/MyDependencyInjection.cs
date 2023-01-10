using HospitalLibrary.ApplicationUsers.Service;
using HospitalLibrary.Appointments.Service;
using HospitalLibrary.BloodConsumptions.Service;
using HospitalLibrary.BloodUnits.Service;
using HospitalLibrary.Common;
using HospitalLibrary.Consiliums.Repository;
using HospitalLibrary.Consiliums.Service;
using HospitalLibrary.Doctors.Repository;
using HospitalLibrary.Doctors.Service;
using HospitalLibrary.EquipmentMovement.Service;
using HospitalLibrary.Examinations.Repository.EventStoreRepository;
using HospitalLibrary.Examinations.Service;
using HospitalLibrary.Examinations.Service.EventStoreService;
using HospitalLibrary.Feedbacks.Repository;
using HospitalLibrary.Feedbacks.Service;
using HospitalLibrary.Holidays.Repository;
using HospitalLibrary.Holidays.Service;
using HospitalLibrary.Medicines.Repository;
using HospitalLibrary.Medicines.Service;
using HospitalLibrary.Patients.Repository;
using HospitalLibrary.Patients.Service;
using HospitalLibrary.Prescriptions.Repository;
using HospitalLibrary.Prescriptions.Service;
using HospitalLibrary.Rooms.Repository;
using HospitalLibrary.Rooms.Service;
using HospitalLibrary.SharedModel.Repository;
using HospitalLibrary.SharedModel.Service;
using HospitalLibrary.TreatmentReports.Repository;
using HospitalLibrary.TreatmentReports.Service;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalAPI.Extensions
{
    public static class MyDependencyInjection
    {
        public static void AddMyDependencyGroup(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<SpecializationsService>();
            services.AddScoped<ISpecializationsRepository, SpecializationsRepository>();
            services.AddScoped<RoomService>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<FeedbackService>();
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            services.AddScoped<IDoctorService,DoctorService>();
            services.AddScoped<ApplicationUserService>();
            services.AddScoped<IWorkingSchueduleRepository, WorkingScheduleRepository>();
            services.AddScoped<WorkingScheduleService>();
            services.AddScoped<PatientService>();
            services.AddScoped<AppointmentService>();
            services.AddScoped<HolidayService>();
            services.AddScoped<ScheduleService>();
            services.AddScoped<BuildingService>();
            services.AddScoped<IBuildingRepository, BuildingRepository>();
            services.AddScoped<FloorService>();
            services.AddScoped<IFloorRepository, FloorRepository>();
            services.AddScoped<GRoomService>();
            services.AddScoped<IGRoomRepository, GRoomRepository>();
            services.AddScoped<IHolidayRepository, HolidayRepository>();
            services.AddScoped<BloodUnitService>();
            services.AddScoped<BloodConsumptionService>();
            services.AddScoped<BuildingService>();
            services.AddScoped<IBuildingRepository, BuildingRepository>();
            services.AddScoped<FloorService>();
            services.AddScoped<IFloorRepository, FloorRepository>();
            services.AddScoped<GRoomService>();
            services.AddScoped<IGRoomRepository, GRoomRepository>();
            services.AddScoped<IHolidayRepository, HolidayRepository>();
            services.AddScoped<BloodUnitService>();
            services.AddScoped<BloodConsumptionService>();
            services.AddScoped<EquipmentService>();
            services.AddScoped<IIEquipmentRepository, EquipmentRepository>();
            services.AddScoped<PatientAdmissionService>();
            services.AddScoped<IPatientAdmissionRepository, PatientAdmissionRepository>();
            services.AddScoped<MaliciousPatientService>();
            services.AddScoped<IMaliciousPatientRepository, MaliciousPatientRepository>();
            services.AddScoped<MedicineService>();
            services.AddScoped<IMedicineRepository, MedicineRepository>();
            services.AddScoped<BloodPrescriptionService>();
            services.AddScoped<IBloodPrescriptionRepository, BloodPrescriptionRepository>();
            services.AddScoped<TreatmentReportService>();
            services.AddScoped<ITreatmentReportRepository, TreatmentReportRepository>();
            services.AddScoped<IRoomBedService,RoomBedService>();
            services.AddScoped<IGeneratePdfReportService,GeneratePdfReportService>();
            services.AddScoped<IRoomBedRepository, RoomBedRepository>();
            services.AddScoped<EquipmentMovementAppointmentService>();
            services.AddScoped<IAllergenRepository, AllergenRepository>();
            services.AddScoped<AllergenService>();
            services.AddScoped<SymptomService>();
            services.AddScoped<IMedicinePrescriptionRepository, MedicinePrescriptionRepository>();
            services.AddScoped<MedicinePrescriptionService>();
            services.AddScoped<IConsiliumService,ConsiliumService>();
            services.AddScoped<IConsiliumRepository, ConsiliumRepository>();
            services.AddScoped<ExaminationService>();
            services.AddScoped<EventStoreExaminationService>();
            services.AddScoped<IEventStoreExaminationRepository, EventStoreExaminationRepository>();


        }
    }
}