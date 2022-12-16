using System;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Dtos.Request;
using HospitalAPI.Dtos.Response;
using HospitalAPI.Infrastructure.Authorization;
using HospitalLibrary.ApplicationUsers.Model;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Appointments.Service;
using HospitalLibrary.CustomException;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Private
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //[HospitalAuthorization(UserRole.Doctor)]
    public class ScheduleController:ControllerBase
    {
        private readonly ScheduleService _scheduleService;
        private readonly IMapper _mapper;

        public ScheduleController(ScheduleService scheduleService, IMapper mapper)
        {
            _scheduleService = scheduleService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AppointmentResponse>> ScheduleAppointment([FromBody] AppointmentRequest appointmentRequest)
        {
            var appointment = _mapper.Map<Appointment>(appointmentRequest);
            var appointmentCreated = await _scheduleService.ScheduleAppointment(appointment);
            var result = _mapper.Map<AppointmentResponse>(appointmentCreated);
            return CreatedAtAction("ScheduleAppointment", new {id = result.Id}, result);
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RescheduleAppointment([FromBody] AppointmentResponse appointmentRequest)
        {
            var appointment = _mapper.Map<Appointment>(appointmentRequest);
            var result = await _scheduleService.RescheduleAppointment(appointment);
            return result == false ? NotFound() : NoContent();
        }
    }
}