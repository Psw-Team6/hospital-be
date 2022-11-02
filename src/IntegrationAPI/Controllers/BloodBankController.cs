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

namespace IntegrationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodBankController: ControllerBase
    {
        private readonly IBloodBankService _bloodBankService;
        private readonly IMapper _mapper;

        public BloodBankController(IBloodBankService bloodBankService, IMapper mapper)
        {
            _bloodBankService = bloodBankService;
            _mapper = mapper;

        }

        // GET: api/bloodbank
        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_bloodBankService.GetAll());
        }

        // GET api/bloodbank/2
        [HttpGet("{id}")]
        public ActionResult GetById(Guid id)
        {
            var room = _bloodBankService.GetById(id);
            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }

        // GET api/bloodbank/BankaKrvi
        [HttpGet("findByName/{name}")]
        public ActionResult GetByName(String name)
        {
            var room = _bloodBankService.GetByName(name);
            if (room == null)
            {
                return Ok(null);
            }

            return Ok(room);
        }

        // POST api/bloodbank
        [HttpPost]
        public ActionResult Create(BloodBankRequest bloodBankRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var bloodBank = _mapper.Map<BloodBank>(bloodBankRequest);
            _bloodBankService.Create(bloodBank);
            return CreatedAtAction("GetById", new { id = bloodBank.Id }, bloodBank);
        }

        // PUT api/bloodbank/2
        [HttpPut("{id}")]
        public ActionResult Update(Guid id, BloodBank bloodBank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bloodBank.Id)
            {
                return BadRequest();
            }

            try
            {
                _bloodBankService.Update(bloodBank);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(bloodBank);
        }

        // DELETE api/bloodbank/2
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var bloodBank = _bloodBankService.GetById(id);
            if (bloodBank == null)
            {
                return NotFound();
            }

            _bloodBankService.Delete(bloodBank);
            return NoContent();
        }

        //GET api/bloodbank/bloodSupply/A/8
        [HttpGet("bloodSupply/{bloodType}/{quantity}")]
        public async Task<BloodSupplyResponse> GetBBSupplyByTypeAndQuantity(String bloodType, String quantity)
        {
            return await GetProductAsync("http://localhost:8080/api/blood/bloodType/" + bloodType +'/'+ quantity);
        }

        static HttpClient client = new HttpClient();


        static async Task<BloodSupplyResponse> GetProductAsync(string path)
        {
            BloodSupplyResponse bloodSupplyResponse = new BloodSupplyResponse(false, 0);

            try
            {
                HttpResponseMessage response = await client.GetAsync(path);
                bloodSupplyResponse.StatusCode = (int)response.StatusCode;
                bloodSupplyResponse.Response= Boolean.Parse(await response.Content.ReadAsStringAsync());
            } catch (HttpRequestException httpEx)
            {
                if (httpEx.StatusCode.HasValue)
                {
                    bloodSupplyResponse.StatusCode = (int)httpEx.StatusCode;
                }
                else
                {
                    if (httpEx.Message.Contains("No connection could be made because the target machine actively refused it."))
                        bloodSupplyResponse.StatusCode = 500;

                }

            }         
            return bloodSupplyResponse;
        }


    }
}
