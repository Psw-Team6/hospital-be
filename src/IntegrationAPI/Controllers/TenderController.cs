using IntegrationAPI.Dtos.Request;
using IntegrationLibrary.BloodBank;
using IntegrationLibrary.Tender.Service;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenderController : ControllerBase
    {
        private readonly ITenderService tenderService;

        public TenderController(ITenderService tenderService)
        {
            this.tenderService = tenderService;
        }

        // POST api/tender/add
        [HttpPost("add")]
        public ActionResult Create(IntegrationLibrary.Tender.Model.Tender tender)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (tender == null)
            {
                return BadRequest();
            }
            tenderService.Create(tender);
            return StatusCode(201, null);
        }
    }
}
