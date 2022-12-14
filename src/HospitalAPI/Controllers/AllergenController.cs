using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HospitalLibrary.SharedModel;
using HospitalLibrary.SharedModel.Service;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AllergenController : ControllerBase
    {
        private readonly AllergenService _allergenService;
        private readonly IMapper _mapper;

        public AllergenController(AllergenService allergenService, IMapper mapper)
        {
            _allergenService = allergenService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Allergen>>> GetAll()
        {
            var admissions = await _allergenService.GetAll();
            var result = _mapper.Map<IEnumerable<Allergen>>(admissions);
            return Ok(result);
        }
    }
}