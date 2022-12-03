using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Dtos.Response;
using HospitalAPI.Infrastructure.Authorization;
using HospitalLibrary.ApplicationUsers.Model;
using HospitalLibrary.Examinations.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Private
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //[HospitalAuthorization(UserRole.Doctor)]
    public class SymptomController:ControllerBase
    {
        private readonly SymptomService _symptomService;
        private readonly IMapper _mapper;

        public SymptomController(SymptomService symptomService, IMapper mapper)
        {
            _symptomService = symptomService;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType( StatusCodes.Status200OK)]
        public async Task<ActionResult<List<SymptomResponse>>> GetAllAppointments()
        {
            var symptoms = await _symptomService.GetAllSymptoms();
            var response = _mapper.Map<List<SymptomResponse>>(symptoms);
            return Ok(response);
        }
    }
}