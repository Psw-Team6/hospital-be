/*using System;
using System.Threading.Tasks;
using HospitalAPI.Dtos.Response;
using HospitalLibrary.sharedModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    public class AddressController : ControllerBase
    {
        [HttpGet("{id}")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Address>> GetById([FromRoute] Guid id)
        {
            //var address = await addressService.GetById(id);
            //var result = _mapper.Map<Address>(address);
            //return result == null ? NotFound() : Ok(result);
        }
    }
}*/