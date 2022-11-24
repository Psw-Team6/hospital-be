using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Dtos.Request;
using HospitalAPI.Dtos.Response;
using HospitalAPI.Infrastructure.Authorization;
using HospitalLibrary.ApplicationUsers.Model;
using HospitalLibrary.EquipmentMovement.Model;
using HospitalLibrary.EquipmentMovement.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [HospitalAuthorization(UserRole.Manager)]
    public class EquipmentMovementAppointmentController:ControllerBase
    {
        private readonly IEquipmentMovementAppointmentService _equipmentMovementAppointmentService;
        private readonly IMapper _mapper;

        public EquipmentMovementAppointmentController(IEquipmentMovementAppointmentService equipmentMovementAppointmentService, IMapper mapper)
        {
            _equipmentMovementAppointmentService = equipmentMovementAppointmentService;
            _mapper = mapper;
        }
        
        [HttpPost]
        [ProducesResponseType( StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EquipmentMovementAppointmentResponse>> Create([FromBody] EquipmentMovementAppointmentResponse equipmentMovementDto)
        {
            var equipmentMovementAppointment = _mapper.Map<EquipmentMovementAppointment>(equipmentMovementDto);
            var equipmentMovementAppointmentCreated = await _equipmentMovementAppointmentService.Create(equipmentMovementAppointment);
            
            var result = _mapper.Map<EquipmentMovementAppointmentResponse>(equipmentMovementAppointmentCreated);
            if (result == null)
            {
                return BadRequest();
            }
            
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EquipmentMovementAppointmentResponse>> GetById(Guid id)
        {
            var equipmentMovementAppointment = await _equipmentMovementAppointmentService.GetById(id);
            var result = _mapper.Map<EquipmentMovementAppointmentResponse>(equipmentMovementAppointment);
            return equipmentMovementAppointment == null ? NotFound() : Ok(result);
        }
        
        
        [HttpPost("getAvailable")]
        [ProducesResponseType(typeof(List<EquipmentMovementAppointmentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<EquipmentMovementAppointmentResponse>>> GetAllAvailableAppointmentsForEquipmentMovement([FromBody]EquipmentMovementAppointmentRequest equipmentAppointmentsRequest)
        {
            var appointmentRequested = _mapper.Map<EquipmentMovementRequest>(equipmentAppointmentsRequest);
            
            var appointments = await _equipmentMovementAppointmentService.GetAllAvailableAppointmentsForEquipmentMovement(appointmentRequested);
            
            var result = _mapper.Map<List<EquipmentMovementAppointmentResponse>>(appointments);
            return result == null ? BadRequest() : Ok(result);
        }
    }
}