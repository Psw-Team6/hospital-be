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

namespace IntegrationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodRequestController : ControllerBase
    {
        private readonly IBloodRequestService _bloodRequestService;
        private readonly IMapper _mapper;

        /*public BloodRequestController(IBloodRequestService bloodRequestService, IMapper mapper)
        {
            _bloodRequestService = bloodRequestService;
            _mapper = mapper;

        }*/

        public BloodRequestController(IBloodRequestService bloodRequestService)
        {
            _bloodRequestService = bloodRequestService;
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
    }
}