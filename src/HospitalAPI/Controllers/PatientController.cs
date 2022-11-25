using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Dtos.Request;
using HospitalAPI.Dtos.Response;
using HospitalAPI.Infrastructure.Authorization;
using HospitalLibrary.ApplicationUsers.Model;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.Patients.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly PatientService _patientService;
        private readonly IMapper _mapper;

        public PatientController( PatientService patientService, IMapper mapper)
        {
            _patientService = patientService;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<PatientResponse>>> GetAllPatients()
        {
            var patients = await _patientService.GetAll();
            var result = _mapper.Map<List<PatientResponse>>(patients);
            return Ok(result);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PatientResponse>> CreatePatient([FromBody] PatientRequest patientRequest)
        {
            var patient = _mapper.Map<Patient>(patientRequest);
            var isUniqueUsername = await _patientService.IsUniqueUsername(patientRequest.Username);
            if (!isUniqueUsername)
            {
                return Conflict();
            }
            var result = await _patientService.CreatePatient(patient);
            return CreatedAtAction(nameof(GetById), new {id = result.Id}, result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PatientResponse>> GetById([FromRoute] Guid id)
        {
            var patient = await _patientService.GetById(id);
            var result = _mapper.Map<PatientResponse>(patient);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("/api/v1/Patient-gender-female")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HospitalAuthorization(UserRole.Manager)]
        public async Task<ActionResult<PatientResponse>> GetFemalePatient()
        {
            var patients = await _patientService.GetFemalePatient();
            return Ok(patients);
        }
        
        [HttpGet("/api/v1/Patient-gender-male")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HospitalAuthorization(UserRole.Manager)]
        public async Task<ActionResult<PatientResponse>> GetMalePatient()
        {
            var patients = await _patientService.GetMalePatient();
            return Ok(patients);
        }
        
        [HttpGet("/api/v1/Patient-gender-other")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HospitalAuthorization(UserRole.Manager)]
        public async Task<ActionResult<PatientResponse>> GetOtherPatient()
        {
            var patients = await _patientService.GetOtherPatient();
            return Ok(patients);
        }

        [HttpGet("/api/v1/Patient-pediatric-group")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HospitalAuthorization(UserRole.Manager)]
        public async Task<ActionResult<PatientResponse>> GetPediatricGroup()
        {
            var patients = await _patientService.GetPediatricGroup();
            return Ok(patients);
        }

        [HttpGet("/api/v1/Patient-young-group")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HospitalAuthorization(UserRole.Manager)]
        public async Task<ActionResult<PatientResponse>> GetYoungGroup()
        {
            var patients = await _patientService.GetYoungGroup();
            return Ok(patients);
        }

        [HttpGet("/api/v1/Patient-middle-age-group")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HospitalAuthorization(UserRole.Manager)]
        public async Task<ActionResult<PatientResponse>> GetMiddleAgeGroup()
        {
            var patients = await _patientService.GetMiddleAgeGroup();
            return Ok(patients);
        }
        
        [HttpGet("/api/v1/Patient-elderly-group")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HospitalAuthorization(UserRole.Manager)]
        public async Task<ActionResult<PatientResponse>> GetElderlyGroup()
        {
            var patients = await _patientService.GetElderlyGroup();
            return Ok(patients);
        }

        [HttpGet("hospitalized-patients")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HospitalAuthorization(UserRole.Doctor)]
        public async Task<ActionResult<IEnumerable<HospitalizedPatientResponse>>> GetAllHospitalizedPatients()
        {
            var hospitalizedPatients = await _patientService.GetAllHospitalizedPatients();
            var result = _mapper.Map<IEnumerable<HospitalizedPatientResponse>>(hospitalizedPatients);
            return result == null ? NotFound() : Ok(result);

        }

        [HttpGet("/api/v1/PatientProfile/{id}")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PatientProfileResponse>> GetProfileById([FromRoute] Guid id)
        {
            var patient = await _patientService.GetById(id);
            var result = _mapper.Map<PatientProfileResponse>(patient);
            return result == null ? NotFound() : Ok(result);
        }
        
         [HttpGet("/api/v1/Doctors-by-pediatric-group")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Dictionary<string, DoctorStatisticsResponse>>> GetDoctorsByPediatricGroup()
        {
            var doctors = await _patientService.GetDoctorsByPediatricGroup();
            var doctorsCount = await _patientService.CountOfDoctors(doctors);
            return Ok(doctorsCount);
        }
        
        [HttpGet("/api/v1/Doctors-by-young-group")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Dictionary<string, DoctorStatisticsResponse>>> GetDoctorsByYoungGroup()
        {
            var doctors = await _patientService.GetDoctorsByYoungGroup();
            var doctorsCount = await _patientService.CountOfDoctors(doctors);
            return Ok(doctorsCount);
        }
        
        [HttpGet("/api/v1/Doctors-by-middle-age-group")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Dictionary<string, DoctorStatisticsResponse>>> GetDoctorsByMiddleAgeGroup()
        {
            var doctors = await _patientService.GetDoctorsByMiddleAgeGroup();
            var doctorsCount = await _patientService.CountOfDoctors(doctors);
            return Ok(doctorsCount);
        }

        [HttpGet("/api/v1/Doctors-by-elderly-group")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Dictionary<string, DoctorStatisticsResponse>>> GetDoctorsByElderlyGroup()
        {
            var doctors = await _patientService.GetDoctorsByElderlyGroup();
            var doctorsCount = await _patientService.CountOfDoctors(doctors);
            return Ok(doctorsCount);
        }
        
        
    }
}