using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Patients.Exceptions;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.Patients.Repository;

namespace HospitalLibrary.Patients.Service
{
    public class PatientHealthStateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPatientHealthStateRepository _patientHealthStateRepository;
        private readonly IPatientHealthStateNotificationRepository _notificationRepository;

        public PatientHealthStateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _patientHealthStateRepository = _unitOfWork.GetRepository<PatientHealthStateRepository>();
            _notificationRepository = _unitOfWork.GetRepository<PatientHealthStateNotificationRepository>();
        }

        public async Task CreatePatientHealthState(PatientHealthState patientHealthState)
        {
            var patient = await _unitOfWork.PatientRepository.GetPatientById(patientHealthState.RootId);
            if (patient == null)
            {
                throw new PatientNotFound("Patient not found");
            }
            patientHealthState.MenstrualCycle.IsValidRange();
            var healthState = new PatientHealthState(patient, patientHealthState.BloodPressure, 
                patientHealthState.BloodSugarLevel, patientHealthState.Weight, patientHealthState.SubmissionDate,
                patientHealthState.MenstrualCycle, patientHealthState.BodyFatPercent);
            healthState.Validate();
            await CheckPatientState(healthState);
            await _patientHealthStateRepository.CreateAsync(healthState);
            await _unitOfWork.CompleteAsync();
        }

        private async Task CheckPatientState(PatientHealthState patientHealthState)
        {
            if (!patientHealthState.CheckPatientState().Any())
            {
                return;
            }

            var notification = new PatientHealthStateNotification(patientHealthState.Root,
                    patientHealthState.CheckPatientState(),DateTime.Now);
            await _notificationRepository.CreateAsync(notification);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<PatientHealthState>> GetAllByPatientId(Guid patientId)
        {
            return await _patientHealthStateRepository.GetAllByPatientId(patientId);
        }

        public async Task<List<PatientHealthStateNotification>> GetAllNotifications(Guid doctorId)
        {
            return await _notificationRepository.GetAllNotifications(doctorId);
        }
    }
}