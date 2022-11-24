using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amqp.Types;
using HospitalLibrary.Common;
using HospitalLibrary.Medicines.Model;
using HospitalLibrary.Patients.Model;

namespace HospitalLibrary.Medicines.Service
{
    public class MedicineService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public MedicineService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<List<Medicine>> GetAllByPatientId()
        {
            return await _unitOfWork.MedicineRepository.GetAllMedicine();
        }
        
        
        
        /*
        public async Task<List<Medicine>> GetAllByPatientId(Guid id)
        {
            Patient patient = await _unitOfWork.PatientRepository.GetPatientById(id);
            List<Medicine> medicine = await _unitOfWork.MedicineRepository.GetAllMedicine();
            List<Medicine> patientMedicines = new List<Medicine>();
            foreach (var m in medicine)
            {
                Boolean hasAllergen = false;
                
                foreach (var ma in m.Allergens)
                {
                    foreach (var a in patient.Allergies)
                    {
                        if (a.Id == ma.Id)
                        {
                            hasAllergen = true;
                        }
                    }
                }

                if (!hasAllergen)
                {
                    patientMedicines.Add(m);
                }
            }

            List<Medicine> newList = new List<Medicine>();
            newList = patientMedicines;
            return newList;
        }
         */

    }
}