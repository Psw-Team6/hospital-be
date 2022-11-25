using System;
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
    public class MedicinePrescriptionController : ControllerBase
    {
        private readonly MedicinePrescriptionService _medicinePrescriptionService;
        private readonly IMapper _mapper;
        
        public MedicinePrescriptionController(MedicinePrescriptionService medicinePrescriptionService, IMapper mapper)
        {
            _medicinePrescriptionService = medicinePrescriptionService;
            _mapper = mapper;
        }
        
        [HttpPost]
        [ProducesResponseType( StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateMedicine(MedicinePrescriptionRequest medicinePrescriptionRequest)
        {
            var medicinePrescription = _mapper.Map<MedicinePrescription>(medicinePrescriptionRequest);
            var blood = await _medicinePrescriptionService.Create(medicinePrescription);
            if (blood == null)
            {
                return NotFound();
            }

            return StatusCode(201, blood);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MedicinePrescription>> GetById([FromRoute] Guid id)
        {
            var patient = await _medicinePrescriptionService.GetPrescriptionById(id);
            var result = _mapper.Map<PatientResponse>(patient);
            return result == null ? NotFound() : Ok(result);
        }
        
    }
}