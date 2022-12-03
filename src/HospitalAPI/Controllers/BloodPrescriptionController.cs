using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Dtos.Request;
using HospitalAPI.Dtos.Response;
using HospitalLibrary.Prescriptions.Model;
using HospitalLibrary.Prescriptions.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BloodPrescriptionController : ControllerBase
    {
        private readonly BloodPrescriptionService _bloodPrescriptionService;
        private readonly IMapper _mapper;
        
        public BloodPrescriptionController( BloodPrescriptionService bloodPrescriptionService, IMapper mapper)
        {
            _bloodPrescriptionService = bloodPrescriptionService;
            _mapper = mapper;
        }
        
        [HttpPost]
        [ProducesResponseType( StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateBlood(BloodPrescriptionRequest bloodRequest)
        {
            var bloodPrescription = _mapper.Map<BloodPrescription>(bloodRequest);
            var blood = await _bloodPrescriptionService.Create(bloodPrescription);
            if (blood == null)
            {
                return NotFound();
            }

            return StatusCode(201, blood);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BloodPrescription>> GetById([FromRoute] Guid id)
        {
            var patient = await _bloodPrescriptionService.GetBloodById(id);
            var result = _mapper.Map<PatientResponse>(patient);
            return result == null ? NotFound() : Ok(result);
        }
        
      
    }
}