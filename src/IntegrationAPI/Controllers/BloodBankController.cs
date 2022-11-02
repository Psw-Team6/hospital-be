using AutoMapper;
using IntegrationAPI.Dtos.Request;
using IntegrationLibrary.BloodBank;
using IntegrationLibrary.BloodBank.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

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
        public async Task<bool> GetBBSupplyByTypeAndQuantity(String bloodType, String quantity)
        {
            return await GetProductAsync("http://localhost:8080/api/blood/bloodType/" + bloodType +'/'+ quantity);
        }

        static HttpClient client = new HttpClient();
        static async Task<bool> GetProductAsync(string path)
        {
            bool hasBlood = false;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            { 
                hasBlood = Boolean.Parse(await response.Content.ReadAsStringAsync());
            }
            return hasBlood;
        }
    }
}
