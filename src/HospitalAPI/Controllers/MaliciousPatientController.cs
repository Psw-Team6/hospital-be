using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
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
    [HospitalAuthorization(UserRole.Manager)]
    public class MaliciousPatientController : ControllerBase
    {
        private readonly MaliciousPatientService _maliciousPatientService;
        private readonly IMapper _mapper;
        
        public MaliciousPatientController(MaliciousPatientService maliciousPatientService, IMapper mapper)
        {
            _maliciousPatientService = maliciousPatientService;
            _mapper = mapper;
        }
        
        [HttpGet("getTrollByPatientId/{patientId:Guid}")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MaliciousPatient>> GetByPatientId([FromRoute]Guid patientId)
        {
            var result = await _maliciousPatientService.GetByPatientId(patientId);
            return result == null ? NotFound() : Ok(result);
        }
        
        [HttpPut("maliciousPatientStatus/{patientId:Guid}")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> MaliciousPatientStatus([FromRoute]Guid patientId)
        {
            var result = await _maliciousPatientService.MaliciousPatientStatus(patientId);
            return result ? Ok(result) : NotFound();
        }
        
        [HttpGet("getAllMaliciousPatients")]
        [ProducesResponseType(typeof(List<MaliciousPatient>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<MaliciousPatient>>> GetAllMaliciousPatients()
        {
            var result = await _maliciousPatientService.GetAllMaliciousPatients();
            return result == null ? NotFound() : Ok(result);
        }
        
    }
}