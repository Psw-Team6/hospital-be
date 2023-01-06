using AutoMapper;
using HospitalAPI.gRPC;
using HospitalLibrary.BloodUnits.Model;
using HospitalLibrary.BloodUnits.Repository;
using HospitalLibrary.BloodUnits.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HospitalAPI.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class UrgentBloodSupplyController : ControllerBase
    {
        private readonly IUrgentBloodSupplyService _urgentBloodSupplyService;
        private readonly IMapper _mapper;
        private readonly BloodUnitService _bloodUnitService;

        public UrgentBloodSupplyController(IUrgentBloodSupplyService urgentBloodSupplyService, IMapper mapper, BloodUnitService bloodUnitService)
        {
            _urgentBloodSupplyService = urgentBloodSupplyService;
            _mapper = mapper;
            _bloodUnitService = bloodUnitService;
        }
        //api/v1/UrgentBloodSupplyController
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BloodUnit>> CreateBloodUnit([FromBody] BloodUnitDto bu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _urgentBloodSupplyService.OrderBloodUrgentlyAsync(bu.BloodType.ToString(),bu.Amount);
            
            return Ok();
        }
    }
}