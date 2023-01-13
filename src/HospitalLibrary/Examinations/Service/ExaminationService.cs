using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Common;
using HospitalLibrary.Examinations.Model;
using HospitalLibrary.Examinations.Repository;
using HospitalLibrary.Examinations.Service.EventStoreService;
using HospitalLibrary.Medicines.Model;
using SendGrid.Helpers.Errors.Model;

namespace HospitalLibrary.Examinations.Service
{
    public class ExaminationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EventStoreExaminationService _eventStoreService;

        public ExaminationService(IUnitOfWork unitOfWork, EventStoreExaminationService eventStoreService)
        {
            _unitOfWork = unitOfWork;
            _eventStoreService = eventStoreService;
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
            var createdExamination =  await _unitOfWork.ExaminationRepository.CreateAsync(newExamination);
            await _unitOfWork.CompleteAsync();
            await _eventStoreService.CreateEventStore(createdExamination,examination.Changes);
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

        public async Task<IEnumerable<Examination>> GetAllExaminations()
        {
            return  await _unitOfWork.GetRepository<ExaminationRepository>().GetAllExaminations();
        }

        private IEnumerable<string> splitQuery(string query)
        {
            IEnumerable<string> splitedQuery = new List<string>();
            
            var split = query.Split("\"");
            int num = 0;
            

            int lencount = 1;
            foreach (var word in split)
            {
                if (num % 2 == 1 && lencount < split.Length)
                {
                    splitedQuery = splitedQuery.Append(word);
                }
                else if(word!="" && word!=" " )
                {
                    var split2 = word.Split(" ");
                    foreach (var word1 in split2)
                    {
                        if (word1 != "" && word1 != " ")
                        {
                            splitedQuery = splitedQuery.Append(word1);

                        }
                    }
                }

                num = num + 1;
                lencount = lencount + 1;
            }
            

            return splitedQuery;
        }

        private bool checkFirstChar(String query)
        {
            var split = query.Split(" ");
            if (split[0][0].Equals('\"') )
            {
                return true;
            }
            return false;
        }
        public async Task<IEnumerable<Examination>> GetSearchedExaminations(String query)
        {
            var allExaminations = await _unitOfWork.ExaminationRepository.GetAllExaminations();
            IEnumerable<Examination> filteredEx = new List<Examination>();
            foreach (var examination in allExaminations)
            {
                if ( CheckSymptoms(query, examination) || CheckMedicine(query, examination) || await CheckDoctor(query, examination) || await CheckPatient(query, examination))
                {
                    filteredEx = filteredEx.Append(examination);
                }
            }

            if (!filteredEx.Any())
            {
                throw new NotFoundException("No Exeminatiosn found");
            }
            return filteredEx;
        }

        private bool CheckMedicine(String query, Examination examination)
        {
            var split = splitQuery(query);
            foreach (var word in split)
            {
                foreach (var prescription in examination.Prescriptions)
                {
                    foreach (var medicine in prescription.Medicines)
                    {
                        if (medicine.Name.ToLower().Contains(word.ToLower()))
                        {
                            return true;
                        }
                        
                    }
                }
            }

            return false;
        }
        
        private bool CheckSymptoms(String query, Examination examination)
        {
            var split = splitQuery(query);
            foreach (var word in split)
            {
                foreach (var symptom in examination.Symptoms)
                {
                    if (symptom.Description.ToLower().Contains(word.ToLower()))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        
        private async Task<bool> CheckPatient(String query, Examination examination)
        {
            var patient = await _unitOfWork.PatientRepository.GetByIdAsync(examination.Appointment.PatientId);
            var split = splitQuery(query);
            foreach (var word in split)
            {
                if (patient.Name.ToLower().Contains(word.ToLower()) || patient.Surname.ToLower().Contains(word.ToLower()))
                {
                    return true;
                }
            }

            return false;
        }
        
        private async Task<bool> CheckDoctor(String query, Examination examination)
        {
            var doctor = await _unitOfWork.DoctorRepository.GetByIdAsync(examination.Appointment.DoctorId);
            var split = splitQuery(query);
            foreach (var word in split)
            {
                if (doctor.Name.ToLower().Contains(word.ToLower()) || doctor.Surname.ToLower().Contains(word.ToLower()))
                {
                    return true;
                }
            }

            return false;
        }
    }
}