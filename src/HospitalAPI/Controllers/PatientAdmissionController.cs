using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Dtos.Request;
using HospitalAPI.Dtos.Response;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.Patients.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    
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
        public async Task<ActionResult<PatientAdmission>> CreateAdmission([FromBody] PatientAdmissionRequest patientAdmissionRequest)
        {
            var admission = _mapper.Map<PatientAdmission>(patientAdmissionRequest);
            var result = await _patientAdmissionService.CreateAdmission(admission);
            if (result == null)
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetById), new {id = result.Id}, result);
        }
        
        
    }
}