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
        
        public UrgentBloodSupplyController(IUrgentBloodSupplyService urgentBloodSupplyService)
        {
            _urgentBloodSupplyService = urgentBloodSupplyService;
           
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