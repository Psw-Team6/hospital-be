using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HospitalLibrary.Common;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Doctors.Repository;

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
            return await _unitOfWork.GetRepository<DoctorRepository>().GetAllDoctors();
        }

        public async Task<Doctor> CreateDoctor(Doctor doctor)
        {
            var newDoctor =await _unitOfWork.GetRepository<DoctorRepository>().CreateAsync(doctor);
            await _unitOfWork.CompleteAsync();
            return newDoctor;
        }

        public async Task<Doctor> GetById(Guid id)
        {
            var doctor = await _unitOfWork.GetRepository<DoctorRepository>().GetByIdAsync(id);
            await _unitOfWork.CompleteAsync();
            return doctor;
        }

        public async Task<bool> DeleteById(Guid id)
        {
            var doctor = await _unitOfWork.GetRepository<DoctorRepository>().GetByIdAsync(id);
            if (doctor == null) { return false; }
            await _unitOfWork.GetRepository<DoctorRepository>().DeleteAsync(doctor);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}