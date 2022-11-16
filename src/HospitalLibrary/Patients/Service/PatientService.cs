using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.ApplicationUsers.Model;
using HospitalLibrary.Common;
using HospitalLibrary.Patients.Model;

namespace HospitalLibrary.Patients.Service
{
    public class PatientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> GetAll()
        {
            return (List<Patient>) await _unitOfWork.PatientRepository.GetAllAsync();
        }

        public async Task<Patient> CreatePatient(Patient patient)
        {
            var hashPassword =PasswordHasher.HashPassword(patient.Password);
            patient.Password = hashPassword;
            patient.UserRole = UserRole.Patient;
            var newPatient = await _unitOfWork.PatientRepository.CreateAsync(patient);
            await _unitOfWork.CompleteAsync();
            return newPatient;
        }

        public async Task<Patient> GetById(Guid id)
        {
            var patient = await _unitOfWork.PatientRepository.GetByIdAsync(id);
            await _unitOfWork.CompleteAsync();
            return patient;
        }
    }
}