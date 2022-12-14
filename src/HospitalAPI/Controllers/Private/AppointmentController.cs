﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Dtos.Response;
using HospitalLibrary.Appointments.Model;
using HospitalAPI.Infrastructure.Authorization;
using HospitalLibrary.ApplicationUsers.Model;
using HospitalLibrary.Appointments.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Private
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //[HospitalAuthorization(UserRole.Doctor)]
    //[HospitalAuthorization(UserRole.Patient)]
    public class AppointmentController : ControllerBase
    {
        private readonly AppointmentService _appointmentService;
        private readonly IMapper _mapper;

        public AppointmentController(AppointmentService appointmentService, IMapper mapper)
        {
            _appointmentService = appointmentService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType( StatusCodes.Status200OK)]
        public async Task<ActionResult<List<AppointmentResponse>>> GetAllAppointments()
        {
            var appointments = await _appointmentService.GetAllAppointments();
            return Ok(appointments);
        }
        
        
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(AppointmentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AppointmentResponse>> GetById([FromRoute]Guid id)
        {
            var appointment = await _appointmentService.GetById(id);
            var result = _mapper.Map<AppointmentResponse>(appointment);
            return result == null ? NotFound() : Ok(result);
        }
        
        [HttpGet("byRoom/{roomId:guid}")]
        [ProducesResponseType(typeof(AppointmentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<AppointmentResponse>>> GetByRoomId([FromRoute]Guid roomId)
        {
            var appointments = await _appointmentService.GetAllByRoomId(roomId);
            return appointments == null ? NotFound() : Ok(appointments);
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CancelAppointment([FromRoute] Guid id)
        {
            var appointment = await _appointmentService.GetById(id);
            if (appointment == null)
                return NotFound();
            
            if(await _appointmentService.CancelAppointment(appointment) == false)
                return BadRequest();
            
            return NoContent();
        }
        
        [HttpGet("GetDoctorAppointments/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<AppointmentResponse>>> GetDoctorAppointments([FromRoute]Guid id)
        {
            var appointments = await _appointmentService.GetDoctorAppointments(id);
            var result = _mapper.Map<List<AppointmentResponse>>(appointments);
            return result == null ? NotFound() : Ok(result);
        }
        
        [HttpGet("GetPatientAppointments/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<AppointmentResponse>>> GetPatientAppointments([FromRoute]Guid id)
        {
            var appointments = await _appointmentService.GetPatientAppointments(id);
            var result = _mapper.Map<List<AppointmentResponse>>(appointments);
            return result == null ? NotFound() : Ok(result);
        }
    }
}