using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Dtos.Request;
using HospitalAPI.Dtos.Response;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Appointments.Service;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
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
        public async Task<ActionResult> GetAllAppointments()
        {
            var appointments = await _appointmentService.GetAllAppointments();
            //var mappedAppointments = _mapper.Map<>
            return Ok(appointments);
        }
        [HttpPost]
        public async Task<ActionResult> CreateAppointment([FromBody] AppointmentRequest appointmentRequest)
        {
            var appointment = _mapper.Map<Appointment>(appointmentRequest);
            var result = await _appointmentService.CreateAppointment(appointment);
            return CreatedAtAction(nameof(GetById), new {id = result.Id}, result);
        }
        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetById([FromRoute]Guid id)
        {
            var appointment =  await _appointmentService.GetById(id);
            var result = _mapper.Map<AppointmentResponse>(appointment);
            return result == null ? NotFound() : Ok(result);
        }
        
        [HttpGet("GetDoctorAppointments/{id:guid}")]
        public async Task<ActionResult> GetDoctorAppointments([FromRoute]Guid id)
        {
            var appointments = await _appointmentService.GetDoctorAppointments(id);
            var result = _mapper.Map<List<AppointmentResponse>>(appointments);
            return result == null ? NotFound() : Ok(result);
        }
    }
}