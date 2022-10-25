using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Dtos;
using HospitalAPI.Dtos.Request;
using HospitalAPI.Dtos.Response;
using HospitalLibrary.Appointments.Service;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Doctors.Service;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly DoctorService _doctorService;
        private readonly IMapper _mapper;

        public DoctorController( DoctorService doctorService, IMapper mapper)
        {
            _doctorService = doctorService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllDoctors()
        {
            var doctors = await _doctorService.GetAll();
            var result = _mapper.Map<List<DoctorResponse>>(doctors);
           return Ok(result);
        }
        
        [HttpPost]
        public async Task<ActionResult> CreateDoctor([FromBody] DoctorRequest doctorRequest)
        {
            var doctor = _mapper.Map<Doctor>(doctorRequest);
            var result = await _doctorService.CreateDoctor(doctor);
            return CreatedAtAction(nameof(GetById), new {id = result.Id}, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            return new NoContentResult();
        }
    }
}