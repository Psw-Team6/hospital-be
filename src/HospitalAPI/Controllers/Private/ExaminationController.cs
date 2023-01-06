using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Dtos.Request;
using HospitalAPI.Dtos.Response;
using HospitalAPI.Infrastructure.Authorization;
using HospitalLibrary.ApplicationUsers.Model;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Examinations.Model;
using HospitalLibrary.Examinations.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Private
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //[HospitalAuthorization(UserRole.Doctor)]
    public class ExaminationController:ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ExaminationService _examinationService;

        public ExaminationController(ExaminationService examinationService, IMapper mapper)
        {
            _examinationService = examinationService;
            _mapper = mapper;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Examination>> CreateExamination([FromBody] ExaminationRequest examinationRequest)
        {
            var examination = _mapper.Map<Examination>(examinationRequest);
            var result = await _examinationService.CreateExamination(examination);
            return CreatedAtAction("CreateExamination", new {id = result.Id}, result);
        }
        
        [HttpGet]
        [ProducesResponseType( StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ExeminationResponse>>> GetAllExaminations()
        {
            var examinations = await _examinationService.GetAllExaminations();
            var result = _mapper.Map<IEnumerable<ExeminationResponse>>(examinations);
            return Ok(result);
        }
    }
}