using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Dtos.Request;
using HospitalAPI.Dtos.Response;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Doctors.Service;
using HospitalLibrary.SharedModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly IMapper _mapper;

        public DoctorController( IDoctorService doctorService, IMapper mapper)
        {
            _doctorService = doctorService;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(typeof(List<DoctorResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<DoctorResponse>>> GetAllDoctors()
        {
            var doctors = await _doctorService.GetAll();
            var result = _mapper.Map<List<DoctorResponse>>(doctors);
           return Ok(result);
        }
        
        [HttpGet("General")]
        [ProducesResponseType(typeof(List<DoctorResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<DoctorResponse>>> GetAllGeneral()
        {
            var doctors = await _doctorService.GetAllGeneralWithRequirements();
            var result = _mapper.Map<List<DoctorResponse>>(doctors);
            return Ok(result);
        }
        
        
        
        [HttpGet("username/{username}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DoctorResponse>> GetByUsername([FromRoute]string username)
        {
            var doctor =  await _doctorService.GetByUsername(username);
            var result = _mapper.Map<DoctorResponse>(doctor);
            return result == null ? NotFound() : Ok(result);
        }
        
        [HttpGet("specialisation/{specialisation}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<DoctorResponse>>> GetBySpecialisation([FromRoute]string specialisation)
        {
            var doctors =  await _doctorService.GetBySpecialisation(specialisation);
            var result = _mapper.Map<List<DoctorResponse>>(doctors);
            return result == null ? NotFound() : Ok(result);
        }
        
        [ProducesResponseType(typeof(List<DateRange>), StatusCodes.Status200OK)]

        [HttpPost("freeRanges/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<DateRange>>> GetFreeTimes([FromRoute]Guid id,[FromBody] DateRange span)
        {
            
            var spanes =  await _doctorService.generateFreeTimeSpans(span,id);
            return Ok(spanes);

        }
        
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DoctorResponse>> CreateDoctor([FromBody] DoctorRequest doctorRequest)
        {
            var doctor = _mapper.Map<Doctor>(doctorRequest);
            var result = await _doctorService.CreateDoctor(doctor);
            return CreatedAtAction(nameof(GetById), new {id = result.Id}, result);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DoctorResponse>> GetById([FromRoute]Guid id)
        {
           var doctor =  await _doctorService.GetById(id);
           var result = _mapper.Map<DoctorResponse>(doctor);
           return result == null ? NotFound() : Ok(result);
        }
        [HttpGet("specialization/{id:guid}")]
        [ProducesResponseType(typeof(DoctorResponse),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DoctorResponse>> GetDoctorSpecialization([FromRoute]Guid id)
        {
           var doctor =  await _doctorService.GetDoctorSpecialization(id);
           var result = _mapper.Map<DoctorResponse>(doctor);
           return result == null ? NotFound() : Ok(result);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteById( Guid id)
        {
            var result = await _doctorService.DeleteById(id);
            return result ? NoContent() : NotFound();
        }
        
        [HttpPost("FreeTermsByDoctorPriority")]
        [ProducesResponseType(typeof(List<AppointmentSuggestion>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<AppointmentSuggestion>>> GetFreeTermsByDoctorPriority([FromBody] AppointmentRangeResponse appointmentRangeResponse)
        {
            AppointmentSuggestion a = new AppointmentSuggestion();
            a.DoctorId = appointmentRangeResponse.DoctorId;
            a.PatientId = appointmentRangeResponse.PatientId;
            a.Duration = appointmentRangeResponse.Duration;
            var ranges = await _doctorService.GetFreeTermsByDoctorPriority(a);
            return ranges == null ? NotFound() : Ok(ranges);
        }
        
        [HttpPost("FreeTermsByTimePriority/{time:bool}")]
        [ProducesResponseType(typeof(List<AppointmentSuggestion>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<AppointmentSuggestion>>> GetFreeTermsByTimePriority([FromRoute]bool time,[FromBody] AppointmentRangeResponse appointmentRangeResponse)
        {
            AppointmentSuggestion a = new AppointmentSuggestion();
            a.DoctorId = appointmentRangeResponse.DoctorId;
            a.PatientId = appointmentRangeResponse.PatientId;
            a.Duration = appointmentRangeResponse.Duration;
            if (time)
            {
                var ranges = await _doctorService.GetFreeTermsByTimeRangePriority(a); 
                return ranges == null ? NotFound() : Ok(ranges);
               
            }

            Console.WriteLine(appointmentRangeResponse);
            var ranges2 = await _doctorService.GetFreeTermsByDoctorPriority(a);
            return ranges2 == null ? NotFound() : Ok(ranges2);
        }
        
       
    }
}