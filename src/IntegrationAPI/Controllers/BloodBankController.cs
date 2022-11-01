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

        public BloodBankController(IBloodBankService bloodBankService) { 
            _bloodBankService = bloodBankService;
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

        // POST api/bloodbank
        [HttpPost]
        public ActionResult Create(BloodBank bloodBank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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
            return await RunAsync("http://localhost:8080/api/blood/bloodType/" + bloodType +'/'+ quantity);
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

        static async Task<bool> RunAsync(string path)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, path);
            request.Headers.Accept.Clear();
            var response = await client.SendAsync(request, CancellationToken.None);

            bool hasBlood = await GetProductAsync(path);
            return hasBlood;
        }
    }
}
