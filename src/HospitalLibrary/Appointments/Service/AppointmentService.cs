using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Appointments.Repository;
using HospitalLibrary.Common;
using HospitalLibrary.Examinations.Model;
using HospitalLibrary.Patients.Service;

namespace HospitalLibrary.Appointments.Service
{
    public class AppointmentService: IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly IGeneratePdfReportService _reportService;

        public AppointmentService(IUnitOfWork unitOfWork, IEmailService emailService, IGeneratePdfReportService reportService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _reportService = reportService;
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointments()
        {
            var appointments =  await _unitOfWork.GetRepository<AppointmentRepository>().GetAllAsync();
            return appointments;
        }
        
        public async Task<Appointment> CreateAppointment(Appointment appointment)
        {
            appointment.AppointmentState = 0;
            appointment.AppointmentType = 0;
            Console.WriteLine(appointment.PatientId.ToString());
            var newAppointment = await _unitOfWork.AppointmentRepository.CreateAsync(appointment);
            await _unitOfWork.CompleteAsync();
            return newAppointment;
        }
        

        public async Task<Appointment> GetById(Guid id)
        {
            return await _unitOfWork.GetRepository<AppointmentRepository>().GetByIdAsync(id);
        }
        
        public async Task<List<Appointment>> GetAllByRoomId(Guid id)
        {
            return await _unitOfWork.GetRepository<AppointmentRepository>().GetAllAppointmentsForRoom(id);
        }
        
        public async Task<bool> CancelAppointment(Appointment appointment)
        {
            if (CanCancelAppointment(appointment))
            {
                appointment.Patient = await _unitOfWork.PatientRepository.GetByIdAsync(appointment.PatientId);
                if (_emailService.SendCancelAppointmentEmail(appointment).Result == null)
                    return false;
                await _unitOfWork.GetRepository<AppointmentRepository>().DeleteAsync(appointment);
                await _unitOfWork.CompleteAsync();
                return true;
            }

            return false;
        }
        
        public async Task<List<Appointment>> GetDoctorAppointments(Guid id)
        {
            var appointments = await _unitOfWork.GetRepository<AppointmentRepository>().GetAllAppointmentsForDoctor(id);
            //var doctorAppointments = appointments.Where(x => x.DoctorId == id);
            return appointments.ToList();
        }
        
        public async Task<List<Appointment>> GetPatientAppointments(Guid id)
        {
            var appointments = await _unitOfWork.GetRepository<AppointmentRepository>().GetAllAppointmentsForPatient(id);
            //var doctorAppointments = appointments.Where(x => x.DoctorId == id);
            return appointments.ToList();
        }
        
        
        private bool CanCancelAppointment(Appointment appointment)
        {
            if(DateTime.Now.AddDays(1).CompareTo(appointment.Duration.From) < 0)
                return true;
            return false;
        }

        public async Task<List<Appointment>> GetAppointmentsForExamination(Guid doctorId)
        {
            var appointments=  await _unitOfWork.AppointmentRepository.GetAppointmentsForExamination(doctorId);
            var sorted = appointments
                .OrderBy(x => x.Duration.To).ToList();
            return sorted;
        }
        public async Task<List<Appointment>> GetNextAppointments(Guid doctorId)
        {
            var appointments=  await _unitOfWork.AppointmentRepository.GetAppointmentsForExamination(doctorId);
            var sorted = appointments
                .Where(app => app.AppointmentState == AppointmentState.Pending)
                .OrderBy(x => x.Duration.From).ToList();
            return sorted;
        }
        
        public async Task<byte[]> GetAppointmentPdfReport(Guid appointmentId, AppointmentReportPdfOptions pdfOptions)
        {
            var appointment = _unitOfWork.AppointmentRepository.GetAppointmentsById(appointmentId).Result;
            if (appointment == null) return null;
            //if (DateTime.Now.CompareTo(appointment.Duration.To) < 0) return null;
            
            var examination =  _unitOfWork.ExaminationRepository.GetExaminationByAppointment(appointment).Result;
            if (examination == null) return null;

            PrepareData(examination,pdfOptions);
            
            return await Task.FromResult(_reportService.GetAppointmentPdfReport(examination));
        }

        public Examination PrepareData(Examination examination,AppointmentReportPdfOptions pdfOptions)
        {
            if (pdfOptions.Anonymized)
                examination.Appointment.Patient = null;

            SetupDataBasedOnOptions(pdfOptions,examination);
            return examination;
        }

        public Examination SetupDataBasedOnOptions(AppointmentReportPdfOptions pdfOptions, Examination examination)
        {
            if (pdfOptions.Presciptions)
                examination = ImportMedicines(examination);
            else 
                examination.Prescriptions = null;
            
            if (!pdfOptions.Symptoms)
                examination.Symptoms = null;
            
            return examination;
        }
        
        public Examination ImportMedicines(Examination examination)
        {
            List<ExaminationPrescription> newPrescriptions = new List<ExaminationPrescription>();
            if (examination.Prescriptions != null)
            {
                foreach (ExaminationPrescription prescription in examination.Prescriptions)
                {
                    newPrescriptions.Add(_unitOfWork.ExaminationPrescriptionRepository
                        .GetPrescriptionById(prescription.Id).Result);
                }

                examination.Prescriptions = newPrescriptions;
            }

            return examination;
        }
    }
}