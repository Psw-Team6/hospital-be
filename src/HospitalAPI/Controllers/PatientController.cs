using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Dtos.Request;
using HospitalAPI.Dtos.Response;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.Patients.Service;
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
        public async Task<ActionResult> GetAllPatients()
        {
            var patients = await _patientService.GetAll();
            var result = _mapper.Map<List<PatientResponse>>(patients);
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<ActionResult> CreateDoctor([FromBody] PatientRequest patientRequest)
        {
            var patient = _mapper.Map<Patient>(patientRequest);
            var result = await _patientService.CreatePatient(patient);
            return CreatedAtAction(nameof(GetById), new {id = result.Id}, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById([FromRoute] Guid id)
        {
            var patient = await _patientService.GetById(id);
            var result = _mapper.Map<PatientResponse>(patient);
            return result == null ? NotFound() : Ok(result);
        }
    }
}