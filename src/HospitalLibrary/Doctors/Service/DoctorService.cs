using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.ApplicationUsers.Model;
using HospitalLibrary.Common;
using HospitalLibrary.Consiliums.Model;
using HospitalLibrary.CustomException;
using HospitalLibrary.Doctors.Model;

namespace HospitalLibrary.Doctors.Service
{
    public class DoctorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Doctor>> GetAll()
        {
            return await _unitOfWork.DoctorRepository.GetAllDoctors();
        }
        
        private async Task<List<Doctor>> GetAllGeneral()
        {
            return await _unitOfWork.DoctorRepository.GetAllDoctorsBySpecialization();
        }

        public async Task<List<Doctor>> GetAllGeneralWithRequirements()
        {
            List<Doctor> doctors = await GetAllGeneral();
            List<Doctor> availableDoctors = new List<Doctor>();
            int minimumPatients = await GetMinimumPatients();
            foreach (var d in doctors)
            {
                if (d.Patients.ToArray().Length <= minimumPatients + 2)
                {
                    availableDoctors.Add(d);
                }
            }
            return availableDoctors;
        }

        private async Task<int> GetMinimumPatients()
        {
            int minimum = 2000000;
            List<Doctor> doctors = await GetAllGeneral();
            foreach (var d in doctors)
            {
                if (d.Patients.ToArray().Length < minimum)
                {
                    minimum = d.Patients.ToArray().Length;
                }
            }
            return minimum;
        }
        public async Task<Doctor> CreateDoctor(Doctor doctor)
        {
            var password = PasswordHasher.HashPassword(doctor.Password);
            doctor.Password = password;
            doctor.UserRole = UserRole.Doctor;
            var newDoctor =await _unitOfWork.DoctorRepository.CreateAsync(doctor);
            
            await _unitOfWork.CompleteAsync();
            return newDoctor;
        }
        public async Task<List<Doctor>> GetDoctorsForConsilium(Consilium consilium)
        {
            var doctors = new List<Doctor>();
            foreach (var doctor in consilium.Doctors)
            {
                var doc = await _unitOfWork.DoctorRepository.GetAllDoctorsBySIdAsync(doctor.Id);
                doctors.Add(doc);
            }
            return doctors;
        }
      

        public async Task<Doctor> GetByUsername(string username)
        {
            var doc = await _unitOfWork.DoctorRepository.GetByUsername(username);
            if (doc == null)
            {
                throw new DoctorNotExist("Doctor not exist");
            }

            return doc;
        }

        public async Task<Doctor> GetById(Guid id)
        {
            var doctor = await _unitOfWork.DoctorRepository.GetByIdAsync(id);
            await _unitOfWork.CompleteAsync();
            return doctor;
        }

        public async Task<bool> DeleteById(Guid id)
        {
            var doctor = await _unitOfWork.DoctorRepository.GetByIdAsync(id);
            if (doctor == null) { return false; }
            await _unitOfWork.DoctorRepository.DeleteAsync(doctor);
            await _unitOfWork.CompleteAsync();
            return true;
        }
        public async Task<List<Doctor>> GetDoctorsBySpecializations(IEnumerable<Specialization> specializations)
        {
            var doctors = new List<Doctor>();
            foreach (var spec in specializations)
            {
                var doctorList = await _unitOfWork.DoctorRepository.GetDoctorsBySpecialization(spec.Id);
                doctors.AddRange(doctorList);
            }
            return doctors;
        }

        public async Task<IEnumerable<Doctor>> GetDoctorsBySpecialization(Guid specId)
        {
            return await _unitOfWork.DoctorRepository.GetDoctorsBySpecialization(specId);
        }
    }
}