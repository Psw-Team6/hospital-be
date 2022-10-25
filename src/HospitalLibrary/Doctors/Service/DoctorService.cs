using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HospitalLibrary.Common;
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

        public async Task<Doctor> CreateDoctor(Doctor doctor)
        {
            var newDoctor =await _unitOfWork.DoctorRepository.CreateAsync(doctor);
            await _unitOfWork.CompleteAsync();
            return newDoctor;
        }
    }
}