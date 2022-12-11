using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Common;
using HospitalLibrary.Examinations.Model;
using HospitalLibrary.Medicines.Model;

namespace HospitalLibrary.Examinations.Service
{
    public class ExaminationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExaminationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Examination>  CreateExamination(Examination examination)
        {
            var app = await _unitOfWork.AppointmentRepository.GetByIdAsync(examination.IdApp);
            var symptoms = await CreateSymptoms(examination);
            var prescriptions = await CreatePrescriptions(examination);
            var newExamination =
                new Examination(app, examination.Anamnesis, symptoms, prescriptions);
            newExamination.ValidateExamination();
            app.AppointmentState = AppointmentState.Finished;
            await _unitOfWork.ExaminationRepository.CreateAsync(newExamination);
            await _unitOfWork.CompleteAsync();
            return newExamination;
        }

        private async Task<List<Symptom>> CreateSymptoms(Examination examination)
        {
            var symptoms = new List<Symptom>();
            foreach (var symptom in examination.Symptoms)
            {
                symptoms.Add(await _unitOfWork.SymptomRepository.GetByIdAsync(symptom.Id));
            }
            return symptoms;
        }
        private async Task<List<ExaminationPrescription>> CreatePrescriptions(Examination examination)
        {
            var prescriptions = new List<ExaminationPrescription>();
            foreach (var prescription in examination.Prescriptions)
            {
                var medicines = new List<Medicine>();
                foreach (var medicine in prescription.Medicines)
                {
                    medicines.Add(await _unitOfWork.MedicineRepository.GetByIdAsync(medicine.Id));
                }
                var examinationPrescription = new ExaminationPrescription(medicines, prescription.Usage);
                prescriptions.Add(examinationPrescription);
            }
            return prescriptions;
        }
    }
}