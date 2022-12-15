using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.Patients.Repository;

namespace HospitalLibrary.Patients.Service
{
    public class MaliciousPatientService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public MaliciousPatientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> MaliciousPatientStatus(Guid maliciousPatientId)
        {
            MaliciousPatient patient = await _unitOfWork.MaliciousPatientRepository.GetByPatientId(maliciousPatientId);
            if (patient == null)
            {
                await CreateMaliciousPatient(maliciousPatientId, DateTime.Now, 1, false);
            }
            else
            {
                if ((DateTime.Now - patient.StartDate).TotalDays < 30)
                {
                    await IncreaseNumberOfCancelation(patient);
                }
                else
                {
                    await DeleteMaliciousPatientsPastDate(patient);
                }
            }
            return true;
        }

        private async Task<bool> CreateMaliciousPatient(Guid patientId, DateTime date, int counter, bool malicious)
        {
            MaliciousPatient mp = new MaliciousPatient();
            mp.PatientId = patientId;
            mp.StartDate = date;
            mp.NumberOfCancellations = 1;
            mp.Malicious = false;
            await _unitOfWork.MaliciousPatientRepository.CreateAsync(mp);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        private async Task<bool> DeleteMaliciousPatientsPastDate(MaliciousPatient maliciousPatient)
        {
            maliciousPatient.StartDate = DateTime.Now;
            maliciousPatient.NumberOfCancellations = 1;
            maliciousPatient.Malicious = false;
            await _unitOfWork.MaliciousPatientRepository.UpdateAsync(maliciousPatient);
            //MaliciousPatient mp = new MaliciousPatient(maliciousPatient.PatientId, DateTime.Now, 1, false);
            //await _unitOfWork.MaliciousPatientRepository.CreateAsync(mp);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        private async Task<bool> IncreaseNumberOfCancelation(MaliciousPatient maliciousPatient)
        {
            maliciousPatient.NumberOfCancellations += 1;
            await _unitOfWork.MaliciousPatientRepository.UpdateAsync(maliciousPatient);
            await _unitOfWork.CompleteAsync();
            await CheckNumberOfCancelations(maliciousPatient);
            return true;
        }

        private async Task<bool> CheckNumberOfCancelations(MaliciousPatient maliciousPatient)
        {
            MaliciousPatient newMaliciousPatient =
                await _unitOfWork.MaliciousPatientRepository.GetByIdAsync(maliciousPatient.Id);
            if (newMaliciousPatient.NumberOfCancellations >= 3)
            {
                maliciousPatient.Malicious = true;
            }

            return true;
        }

        public async Task<MaliciousPatient> GetByPatientId(Guid patientId)
        {
            return await _unitOfWork.MaliciousPatientRepository.GetByPatientId(patientId);
        }
        
        public async Task<IEnumerable<MaliciousPatient>> GetAllMaliciousPatients()
        {
            return await _unitOfWork.MaliciousPatientRepository.GetAllMaliciousPatients();
        }
    }
}