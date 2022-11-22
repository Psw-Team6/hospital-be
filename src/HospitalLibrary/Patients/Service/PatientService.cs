using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.ApplicationUsers.Model;
using HospitalLibrary.Common;
using HospitalLibrary.Patients.Enums;
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
            patient.Address =  await _unitOfWork.AddressRepository.CreateAsync(patient.Address);
            patient.AddressId = patient.Address.Id;
            patient.Password = hashPassword;
            patient.UserRole = UserRole.Patient;
            patient.Doctor = await _unitOfWork.DoctorRepository.GetByIdAsync(patient.DoctorId);
            patient.Enabled = false;
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
        
        public async Task<int> GetFemalePatient()
        {
            var patients = (List<Patient>) await _unitOfWork.PatientRepository.GetAllAsync();
            int counter = 0;
            foreach (var p in patients)
            {
                if (p.Gender == Gender.FEMALE)
                {
                    ++counter;
                }
            }

            return counter;
        }
        
        public async Task<int> GetMalePatient()
        {
            var patients = (List<Patient>) await _unitOfWork.PatientRepository.GetAllAsync();
            int counter = 0;
            foreach (var p in patients)
            {
                if (p.Gender == Gender.MALE)
                {
                    ++counter;
                }
            }

            return counter;
        }
        
        public async Task<int> GetOtherPatient()
        {
            var patients = (List<Patient>) await _unitOfWork.PatientRepository.GetAllAsync();
            int counter = 0;
            foreach (var p in patients)
            {
                if (p.Gender == Gender.OTHER)
                {
                    ++counter;
                }
            }

            return counter;
        }

        public async Task<int> GetPediatricGroup()
        {
            var patients = (List<Patient>) await _unitOfWork.PatientRepository.GetAllAsync();
            int counter = 0;
            foreach (var p in patients)
            {
                if (p.Age >= 0 && p.Age <= 14)
                {
                    ++counter;
                }
            }

            return counter;
        }

        public async Task<int> GetYoungGroup()
        {
            var patients = (List<Patient>) await _unitOfWork.PatientRepository.GetAllAsync();
            int counter = 0;
            foreach (var p in patients)
            {
                if (p.Age >= 15 && p.Age <= 47)
                {
                    ++counter;
                }
            }

            return counter;
        }
        
        public async Task<int> GetMiddleAgeGroup()
        {
            var patients = (List<Patient>) await _unitOfWork.PatientRepository.GetAllAsync();
            int counter = 0;
            foreach (var p in patients)
            {
                if (p.Age >= 48 && p.Age <= 63)
                {
                    ++counter;
                }
            }

            return counter;
        }

        public async Task<int> GetElderlyGroup()
        {
            var patients = (List<Patient>) await _unitOfWork.PatientRepository.GetAllAsync();
            int counter = 0;
            foreach (var p in patients)
            {
                if (p.Age >= 64)
                {
                    ++counter;
                }
            }

            return counter;
        }
    }
}