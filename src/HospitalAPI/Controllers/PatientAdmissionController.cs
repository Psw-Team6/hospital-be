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
    [HospitalAuthorization(UserRole.Doctor)]
    public class PatientAdmissionController : ControllerBase
    {
        private readonly PatientAdmissionService _patientAdmissionService;
        private readonly IMapper _mapper;
        
        public PatientAdmissionController(PatientAdmissionService patientAdmissionService, IMapper mapper)
        {
            _patientAdmissionService = patientAdmissionService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientAdmission>>> GetAll()
        {
            var admissions = await _patientAdmissionService.GetAll();
            var result = _mapper.Map<IEnumerable<PatientAdmission>>(admissions);
            return Ok(result);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PatientAdmission>> GetById([FromRoute] Guid id)
        {
            var admission = await _patientAdmissionService.GetById(id);
            var result = _mapper.Map<PatientAdmission>(admission);
            return result == null ? NotFound() : Ok(result);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PatientAdmissionResponse>> CreateAdmission([FromBody] PatientAdmissionRequest patientAdmissionRequest)
        {
            var admission = _mapper.Map<PatientAdmission>(patientAdmissionRequest);
            var isHospitalized = await _patientAdmissionService.IsHospitalized(admission);
            if (isHospitalized)
            {
                return Forbid();
            }
            var result = await _patientAdmissionService.CreateAdmission(admission);
            if (result == null)
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetById), new {id = result.Id}, result);
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DischargePatient([FromBody] DischargePatientAdmissionRequest patientAdmission)
        {
            var admission = _mapper.Map<PatientAdmission>(patientAdmission);
            var result =  await _patientAdmissionService.DischargePatient(admission);
            return result == false ? NotFound() : NoContent();
        }
        [HttpGet("admission/{id}")]
        [ProducesResponseType( typeof(PatientAdmissionResponse),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PatientAdmissionResponse>> GetByAdmissionIdAndIncludePatient([FromRoute] Guid id)
        {
            var admission = await _patientAdmissionService.GetAdmissionWithPatientById(id);
            var result = _mapper.Map<PatientAdmissionResponse>(admission);
            return result == null ? NotFound() : Ok(result);
        }
    }
}