using AutoMapper;
using HospitalAPI.Dtos.Request;
using HospitalAPI.Dtos.Response;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.BloodConsumptions.Model;
using HospitalLibrary.BloodUnits.Model;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.EquipmentMovement.Model;
using HospitalLibrary.Examinations.Model;
using HospitalLibrary.Feedbacks.Model;
using HospitalLibrary.Holidays.Model;
using HospitalLibrary.Medicines.Model;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.Prescriptions.Model;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.SharedModel;
using HospitalLibrary.TreatmentReports.Model;

namespace HospitalAPI.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
            {
                CreateMap<SpecializationResponse, Specialization>();
                CreateMap<Specialization, SpecializationResponse>();
                CreateMap<Specialization, SpecializationRequest>();
                CreateMap<SpecializationRequest, Specialization>();
                CreateMap<Doctor, DoctorResponse>();
                CreateMap<DoctorRequest,Doctor>();
                CreateMap<PatientRequest,Patient>();
                CreateMap<Patient,PatientResponse>();
                CreateMap<RoomResponse,Room>();
                CreateMap<RoomRequest,Room>();
                CreateMap<FeedbackRequest, Feedback>();
                CreateMap<Feedback, FeedbackResponse>();
                CreateMap<AddressResponse,Address>();
                CreateMap<Address,AddressResponse>();
                CreateMap<Room,RoomResponse>();
                CreateMap<AppointmentRequest, Appointment>();
                CreateMap<HolidayResponse, Holiday>();
                CreateMap<Holiday,HolidayResponse>();
                CreateMap<HolidayRequest, Holiday>();
                CreateMap<Holiday,HolidayRequest>();
                CreateMap<Appointment, AppointmentRequest>();
                CreateMap<Appointment,AppointmentResponse>();
                CreateMap<AppointmentResponse, Appointment>();
                CreateMap<WorkingScheduleRequest,WorkingSchedule>();
                CreateMap<FeedbackResponse, Feedback>();
                CreateMap<Feedback, FeedbackRequest>();
                CreateMap<Patient, PatientResponseName>();
                CreateMap<Feedback, FeedbackStatusRequest>();
                CreateMap<Feedback, FeedbackStatusResponse>();
                CreateMap<FeedbackStatusResponse, Feedback>();
                CreateMap<FeedbackStatusRequest, Feedback>();
                CreateMap<BuildingResponse, Building>();
                CreateMap<Building, BuildingResponse>();
                CreateMap<FloorResponse, Floor>();
                CreateMap<Floor, FloorResponse>();
                CreateMap<RoomEquipmentResponse, RoomEquipment>();
                CreateMap<RoomEquipment, RoomEquipmentResponse>();
                CreateMap<FloorRequest, Floor>();
                CreateMap<Floor, FloorRequest>();
                CreateMap<RoomEquipmentRequest, RoomEquipment>();
                CreateMap<RoomEquipment, RoomEquipmentRequest>();
                CreateMap<BuildingRequest, Building>();
                CreateMap<Building, BuildingRequest>();
                CreateMap<BloodConsumationRequest, BloodConsumptionCreateDto>();
                CreateMap<PatientAdmissionRequest, PatientAdmission>();
                CreateMap<PatientAdmission, PatientAdmissionResponse>();
                CreateMap<PatientAdmissionResponse, PatientAdmission>();
                CreateMap<PatientAdmission, PatientAdmissionRequest>();
                CreateMap<EquipmentMovementAppointmentResponse, EquipmentMovementAppointment>();
                CreateMap<EquipmentMovementAppointment, EquipmentMovementAppointmentResponse>();
                CreateMap<EquipmentMovementAppointmentRequest, EquipmentMovementRequest>();
                CreateMap<EquipmentMovementRequest, EquipmentMovementAppointmentRequest>();
                CreateMap<DischargePatientAdmissionRequest, PatientAdmission>();
                CreateMap<Patient, HospitalizedPatientResponse>();
                CreateMap<PatientAdmission, HospitalizePatientAdmissionResponse>();
                CreateMap<PatientProfileRequest, Patient>();
                CreateMap<Patient, PatientProfileResponse>();
                CreateMap<PatientProfileResponse, Patient>();
                CreateMap<Patient, PatientProfileRequest>();
                CreateMap<Symptom, SymptomResponse>();
                CreateMap<SymptomResponse, Symptom>();
                CreateMap<TreatmentReport, TreatmentReportBloodRequest>();
                CreateMap<BloodPrescriptionRequest, BloodPrescription>();
                CreateMap<BloodPrescription, BloodPrescriptionRequest>();
                CreateMap<BloodUnitDto, BloodUnit>();
                CreateMap<BloodUnit, BloodUnitDto>();
                CreateMap<TreatmentReportIdResponse, TreatmentReport>();
                CreateMap<TreatmentReport, TreatmentReportIdResponse>();
                CreateMap<MedicinePrescriptionRequest, MedicinePrescription>();
                CreateMap<MedicinePrescription, MedicinePrescriptionRequest>();
                CreateMap<MedicineResponse, Medicine>();
                CreateMap<Medicine, MedicineResponse>();
                CreateMap<Medicine, MedicineExaminationResponse>();
                CreateMap<MedicineExaminationResponse, Medicine>();
                CreateMap<ExaminationRequest, Examination>();
                CreateMap<Examination, ExaminationRequest>();
                CreateMap<ExaminationPrescriptionRequest, ExaminationPrescription>();
                CreateMap<ExaminationPrescription, ExaminationPrescriptionRequest>();
            }
    }
    
}