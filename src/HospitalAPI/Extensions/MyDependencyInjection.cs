using HospitalLibrary.ApplicationUsers.Service;
using HospitalLibrary.Appointments.Service;
using HospitalLibrary.BloodConsumptions.Service;
using HospitalLibrary.BloodUnits.Service;
using HospitalLibrary.Common;
using HospitalLibrary.Doctors.Repository;
using HospitalLibrary.Doctors.Service;
using HospitalLibrary.EquipmentMovement.Model;
using HospitalLibrary.EquipmentMovement.Repository;
using HospitalLibrary.EquipmentMovement.Service;
using HospitalLibrary.Feedbacks.Repository;
using HospitalLibrary.Feedbacks.Service;
using HospitalLibrary.Holidays.Repository;
using HospitalLibrary.Holidays.Service;
using HospitalLibrary.Patients.Repository;
using HospitalLibrary.Patients.Service;
using HospitalLibrary.Rooms.Repository;
using HospitalLibrary.Rooms.Service;
using HospitalLibrary.sharedModel.Repository;
using HospitalLibrary.sharedModel.Service;
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
            services.AddScoped<DoctorService>();
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
            services.AddScoped<TreatmentReportService>();
            services.AddScoped<ITreatmentReportRepository, TreatmentReportRepository>();
            services.AddScoped<RoomBedService>();
            services.AddScoped<GeneratePdfReportService>();
            services.AddScoped<IRoomBedRepository, RoomBedRepository>();
            services.AddScoped<EquipmentMovementAppointmentService>();
            services.AddScoped<IEquipmentMovementAppointmentRepository, EquipmentMovementAppointmentRepository>();
            services.AddScoped<IAllergenRepository, AllergenRepository>();
            services.AddScoped<AllergenService>();
        }
    }
}