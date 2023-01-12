using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Dtos.Request;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.Patients.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PatientHealthStateController:ControllerBase
    {
        private readonly PatientHealthStateService _patientHealthStateService;
        private readonly IMapper _mapper;

        public PatientHealthStateController(PatientHealthStateService patientHealthStateService, IMapper mapper)
        {
            _patientHealthStateService = patientHealthStateService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async  Task<ActionResult> CreatePatientHealthState([FromBody] PatientHealthStateDto patientHealthStateDto)
        {
            var result = _mapper.Map<PatientHealthState>(patientHealthStateDto);
            await _patientHealthStateService.CreatePatientHealthState(result);
            return CreatedAtAction(nameof(CreatePatientHealthState), "", patientHealthStateDto);
        }
        [HttpGet("/GetByPatient/{patientId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<PatientHealthStateDto>>> GetByPatientId([FromRoute]Guid patientId)
        {
            var states = await _patientHealthStateService.GetAllByPatientId(patientId);
            var result = _mapper.Map<List<PatientHealthStateDto>>(states);
            return result.Any() ? Ok(result) : NotFound();
        }

    }
}