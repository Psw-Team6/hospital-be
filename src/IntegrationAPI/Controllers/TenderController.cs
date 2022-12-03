using IntegrationAPI.Dtos.Request;
using IntegrationLibrary.BloodBank;
using IntegrationLibrary.Tender.Service;
using Microsoft.AspNetCore.Mvc;
using System;

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

        // GET api/tender/sdal5dfs
        [HttpGet("{id}")]
        public ActionResult GetById(Guid id)
        {
            var tender = tenderService.GetById(id);
            if (tender == null)
            {
                return NotFound();
            }

            return Ok(tender);
        }

        // GET api/tender/confirm/sdal5dfs
        [HttpGet("confirm/{id}")]
        public ActionResult confirmTender(Guid id)
        {
            var tender = tenderService.GetById(id);
            if (tender == null)
            {
                return NotFound();
            }


            tender.Status = IntegrationLibrary.Enums.StatusTender.Close;
            tenderService.Update(tender);
            return Ok(tender);
        }

    }
}
