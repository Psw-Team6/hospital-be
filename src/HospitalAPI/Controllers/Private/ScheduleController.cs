using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Dtos.Request;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Appointments.Service;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Private
{
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
        public async Task<ActionResult> CreateAppointment([FromBody] AppointmentRequest appointmentRequest)
        {
            var appointment = _mapper.Map<Appointment>(appointmentRequest);
            var result = await _scheduleService.ScheduleAppointment(appointment);
            return CreatedAtAction("CreateAppointment", new {id = result.Id}, result);
        }
    }
}