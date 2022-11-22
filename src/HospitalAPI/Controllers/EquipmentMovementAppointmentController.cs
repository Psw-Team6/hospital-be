using System;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Dtos.Request;
using HospitalAPI.Dtos.Response;
using HospitalLibrary.EquipmentMovement.Model;
using HospitalLibrary.EquipmentMovement.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    public class EquipmentMovementAppointmentController:ControllerBase
    {
        private readonly EquipmentMovementAppointmentService _equipmentMovementAppointmentService;
        private readonly IMapper _mapper;

        public SpecializationsController(EquipmentMovementAppointmentService equipmentMovementAppointmentService, IMapper mapper)
        {
            _equipmentMovementAppointmentService = equipmentMovementAppointmentService;
            _mapper = mapper;
        }
        
        [HttpPost]
        [ProducesResponseType( StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EquipmentMovementAppointmentResponse>> Create(EquipmentMovementAppointmentRequest equipmentMovementDto)
        {
            var equipmentMovementAppointment = _mapper.Map<EquipmentMovementAppointment>(equipmentMovementDto);
            var equipmentMovementAppointmentCreated = await _equipmentMovementAppointmentService.Create(equipmentMovementAppointment);
            var result = _mapper.Map<EquipmentMovementAppointmentResponse>(equipmentMovementAppointmentCreated);
            return CreatedAtAction(nameof(GetById), new { id = result.id }, result);
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
    }
}