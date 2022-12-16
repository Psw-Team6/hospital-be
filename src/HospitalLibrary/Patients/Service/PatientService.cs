using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.ApplicationUsers.Model;
using HospitalLibrary.Common;
using HospitalLibrary.Doctors.Service;
using HospitalLibrary.Patients.Enums;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.SharedModel;

namespace HospitalLibrary.Patients.Service
{
    public class PatientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDoctorService _doctorService;

        public PatientService(IUnitOfWork unitOfWork, IDoctorService doctorService)
        {
            _unitOfWork = unitOfWork;
            _doctorService = doctorService;
        }

        public async Task<object> GetAll()
        {
            return (List<Patient>)await _unitOfWork.PatientRepository.GetAllPatients();
        }

        public async Task<Boolean> IsUniqueUsername(String username)
        {
            ApplicationUser user = await _unitOfWork.UserRepository.FindByUsername(username);
            if (user == null)
            {
                return true;
            }

            return false;
        }

        public async Task<Patient> CreatePatient(Patient patient)
        {
            var hashPassword = PasswordHasher.HashPassword(patient.Password);
            patient.Address = await _unitOfWork.AddressRepository.CreateAsync(patient.Address);
            patient.AddressId = patient.Address.Id;
            patient.Password = hashPassword;
            patient.UserRole = UserRole.Patient;
            patient.IsBlocked = false;
            patient.CalculateAge();
            patient.Jmbg = new Jmbg(patient.Jmbg.Text);
            patient.Jmbg.ValidateJmbg();
            List<Allergen> allergens = new List<Allergen>();
            foreach (var id in patient.AllergyIds)
            {
                Guid newGuid = Guid.Parse(id);

                allergens.Add(await _unitOfWork.AllergenRepository.GetByIdAsync(newGuid));

            }

            patient.Allergies = allergens;
            patient.Doctor = await _unitOfWork.DoctorRepository.GetByIdAsync(patient.DoctorId);
            patient.Enabled = true;
            var newPatient = await _unitOfWork.PatientRepository.CreateAsync(patient);
            await _unitOfWork.CompleteAsync();
            return newPatient;
        }

        public async Task<Patient> GetById(Guid id)
        {
            var patient = await _unitOfWork.PatientRepository.GetPatientById(id);
            await _unitOfWork.CompleteAsync();
            return patient;
        }
        
        public async Task<bool> Update(Patient patient)
        {
            await _unitOfWork.PatientRepository.UpdateAsync(patient);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<int> GetFemalePatient()
        {
            var patients = (List<Patient>)await _unitOfWork.PatientRepository.GetAllAsync();
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
            var patients = (List<Patient>)await _unitOfWork.PatientRepository.GetAllAsync();
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
            var patients = (List<Patient>)await _unitOfWork.PatientRepository.GetAllAsync();
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
            var patients = (List<Patient>)await _unitOfWork.PatientRepository.GetAllAsync();
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
            var patients = (List<Patient>)await _unitOfWork.PatientRepository.GetAllAsync();
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
            var patients = (List<Patient>)await _unitOfWork.PatientRepository.GetAllAsync();
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
            var patients = (List<Patient>)await _unitOfWork.PatientRepository.GetAllAsync();
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

        public async Task<IEnumerable<Patient>> GetAllHospitalizedPatients()
        {
            var hospitalizedPatients = await _unitOfWork.PatientRepository.GetAllHospitalizedPatientsAsync();
            var currentHospitalizedPatients = new List<Patient>();
            if (currentHospitalizedPatients == null)
                throw new ArgumentNullException(nameof(currentHospitalizedPatients));
            hospitalizedPatients.ToList()
                .ForEach(patient =>
                    {
                        patient.PatientAdmissions.ToList().ForEach(admission =>
                        {
                            if (admission.DateOfDischarge == null)
                            {
                                currentHospitalizedPatients.Add(patient);
                            }
                        });
                    }
                );
            return currentHospitalizedPatients;
        }

        public async Task<List<string>> GetDoctorsByPediatricGroup()
        {
            var patients = (List<Patient>)await _unitOfWork.PatientRepository.GetAllAsync();
            List<string> doctorsList = new List<string>();
            foreach (var p in patients)
            {
                if (p.Age >= 0 && p.Age <= 14)
                {
                    var doctor = _doctorService.GetById(p.DoctorId);
                    doctorsList.Add(doctor.Result.Name);
                }

            }

            return doctorsList;
        }

        public async Task<Dictionary<string, DoctorStatistics>> CountOfDoctors(List<string> doctors)
        {
            Dictionary<string, DoctorStatistics> favouriteDoctors = new Dictionary<string, DoctorStatistics>();
            foreach (var doctorName in doctors)
            {
                if (favouriteDoctors.ContainsKey(doctorName))
                {
                    favouriteDoctors[doctorName].DoctorCount = ++favouriteDoctors[doctorName].DoctorCount;
                    continue;
                }

                DoctorStatistics ds = new DoctorStatistics();
                ds.DoctorCount = ++ds.DoctorCount;
                favouriteDoctors.Add(doctorName, ds);
            }

            return await Task.FromResult(favouriteDoctors);
        }

        public async Task<List<string>> GetDoctorsByYoungGroup()
        {
            var patients = (List<Patient>)await _unitOfWork.PatientRepository.GetAllAsync();
            List<string> doctorsList = new List<string>();
            foreach (var p in patients)
            {
                if (p.Age >= 15 && p.Age <= 47)
                {
                    var doctor = _doctorService.GetById(p.DoctorId);
                    doctorsList.Add(doctor.Result.Name);
                }
            }



            return doctorsList;
        }

        public async Task<List<string>> GetDoctorsByMiddleAgeGroup()
        {
            var patients = (List<Patient>)await _unitOfWork.PatientRepository.GetAllAsync();
            List<string> doctorsList = new List<string>();
            foreach (var p in patients)
            {
                if (p.Age >= 48 && p.Age <= 63)
                {
                    var doctor = _doctorService.GetById(p.DoctorId);
                    doctorsList.Add(doctor.Result.Name);
                }
            }



            return doctorsList;
        }

        public async Task<List<string>> GetDoctorsByElderlyGroup()
        {
            var patients = (List<Patient>)await _unitOfWork.PatientRepository.GetAllAsync();
            List<string> doctorsList = new List<string>();
            foreach (var p in patients)
            {
                if (p.Age >= 64)
                {
                    var doctor = _doctorService.GetById(p.DoctorId);
                    doctorsList.Add(doctor.Result.Name);
                }
            }
            return doctorsList;
        }
    }
}