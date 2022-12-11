using AutoMapper;
using IntegrationAPI.Dtos.Request;
using IntegrationLibrary.BloodBank;
using IntegrationLibrary.BloodBank.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using IntegrationAPI.Dtos.Response;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using IntegrationLibrary.BloodRequests.Service;
using IntegrationLibrary.BloodRequests.Model;
using IntegrationLibrary.HTTP;
using BloodSupplyResponse = IntegrationLibrary.BloodRequests.Model.BloodSupplyResponse;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodRequestController : ControllerBase
    {
        private readonly IBloodRequestService _bloodRequestService;
        private readonly IBloodBankService _bloodBankService;
        private readonly IHttpService _httpService;
        private readonly IMapper _mapper;

        public BloodRequestController(IBloodRequestService bloodRequestService)
        {
            _bloodRequestService = bloodRequestService;

        }
        [ActivatorUtilitiesConstructor]
        public BloodRequestController(IBloodRequestService bloodRequestService, IBloodBankService bloodBankService, IHttpService httpService)
        {
            _bloodRequestService = bloodRequestService;
            _bloodBankService = bloodBankService;
            _httpService = httpService;
        }

        // GET: api/BloodRequest
        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_bloodRequestService.GetAll());
        }

        // GET: api/BloodRequest/pending
        [HttpGet("pending")]
        public ActionResult GetAllOnPending()
        {
            return Ok(_bloodRequestService.GetAllOnPending());
        }

        // GET: api/BloodRequest/returned/Ilija
        [HttpGet("returned/{username}")]
        public ActionResult GetAllReturned(String username)
        {
            return Ok(_bloodRequestService.GetAllReturned(username));
        }

        // GET: api/BloodRequest
        [HttpGet("getFirst")]
        public BloodRequest GetFirst()
        {
            return (BloodRequest)_bloodRequestService.GetFirst();
        }

        // POST api/BloodRequest
        [HttpPost]
        public ActionResult Create(BloodRequest bloodRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _bloodRequestService.Create(bloodRequest);
            //return CreatedAtAction("GetById", new { id = bloodRequest.Id }, bloodRequest);
            return StatusCode(201, null);
        }

        // PUT api/BloodRequest/update
        [HttpPut("update")]

        public ActionResult Update(BloodRequest bloodRequest)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _bloodRequestService.Update(bloodRequest);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(bloodRequest);
        }

        //GET api/bloodbank/bloodSupply/A/8
        [HttpPut("sendBloodRequest/{bloodBankName}")]
        public async Task<BloodSupplyResponse> sendBloodRequest(BloodRequest bloodRequest, String bloodBankName)
        {

            if (!_bloodRequestService.IfOnDemandRequest(bloodRequest))
            {
                //Schedule send
                BloodBank bloodBank = _bloodBankService.GetByName(bloodBankName);
                bloodRequest.Status = Status.APPPROVED;
                bloodRequest.BloodBankId = bloodBank.Id;
                Update(bloodRequest);

                return null;
            }
            else
            {
                BloodBank bloodBank = _bloodBankService.GetByName(bloodBankName);
                bloodRequest.Status = Status.SENT;
                bloodRequest.BloodBankId = bloodBank.Id;
                Update(bloodRequest);

                if (!ModelState.IsValid)
                {
                    return null;
                }

                try
                {
                    //ON-DEMAND
                    return await _httpService.GetProductAsync(bloodBank?.ServerAddress + "blood/" + bloodBankName + "/" + bloodRequest.Type + '/' + bloodRequest.Amount);
                }
                catch
                {
                    return null;
                }
            }
            
        }


    }
}