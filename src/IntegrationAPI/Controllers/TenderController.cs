using IntegrationAPI.Dtos.Request;
using IntegrationLibrary.BloodBank;
using IntegrationLibrary.Tender.Model;
using IntegrationLibrary.Tender.Service;
using Microsoft.AspNetCore.Mvc;
using System;
<<<<<<< HEAD
=======
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
>>>>>>> 34587da71ecd72bf1c5f8be79ffe9b02d1df840d

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

        [HttpGet("all")]
        public async Task<ActionResult<List<IntegrationLibrary.Tender.Model.Tender>>> GetAll()
        {
            var tenders = await tenderService.GetAll();
            return Ok( tenders);
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
            tender.PublishedDate = DateTime.Now;
            tenderService.Create(tender);
            return StatusCode(201, null);
        }

<<<<<<< HEAD
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
=======
        [HttpPut("addOffer")]
        public ActionResult AddTenderOffer(TenderOfferRequest tenderOfferReq)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (tenderOfferReq == null)
            {
                return BadRequest();
            }
            TenderOffer tenderOffer = tenderOfferReq.convertTenderOffer();
            if (!tenderOffer.isBloodBankNameNotEmpty() && !tenderOffer.isRealizationDateInFuture() && !tenderOffer.isThePricePositive())
            {
                return BadRequest();
            }

            Tender tenderS = tenderService.GetById(tenderOfferReq.Tender.Id);
            tenderS.addTenderOffer(tenderOffer);
            tenderService.Update(tenderS);
            return StatusCode(201, null);
>>>>>>> 34587da71ecd72bf1c5f8be79ffe9b02d1df840d
        }

    }


}
