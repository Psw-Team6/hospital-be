using IntegrationAPI.Dtos.Request;
using IntegrationLibrary.BloodBank;
using IntegrationLibrary.BloodBank.Service;
using IntegrationLibrary.SendMail;
using IntegrationLibrary.SendMail.Services;
using IntegrationLibrary.Tender.Model;
using IntegrationLibrary.Tender.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenderController : ControllerBase
    {
        private readonly ITenderService tenderService;
        private readonly IBloodBankService bankService;
        private readonly IEmailService emailService;


        public TenderController(ITenderService tenderService)
        {
            this.tenderService = tenderService;
        }


        [ActivatorUtilitiesConstructor]
        public TenderController(ITenderService tenderService, IBloodBankService bankService, IEmailService emailService)
        {
            this.tenderService = tenderService;
            this.bankService = bankService;
            this.emailService = emailService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<IntegrationLibrary.Tender.Model.Tender>>> GetAll()
        {
            var tenders = await tenderService.GetAll();
            return Ok(tenders);
        }

        [HttpGet("byId/{tenderID}")]
        public Tender GetById(string tenderID)
        {
            Tender tender = tenderService.GetById(Guid.Parse(tenderID));
            return tender;
        }

        [HttpPut]

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

        }

        [HttpPut("confirm")]
        public ActionResult ConfirmTender(Tender tender)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (tender == null)
            {
                return BadRequest();
            }
            tender.Status = IntegrationLibrary.Enums.StatusTender.Close;
            tenderService.Update(tender);
            List<TenderOffer> tenderOffers = (List<TenderOffer>)tender.TenderOffer;
            foreach(TenderOffer to in tenderOffers)
            {
                if (tender.Winner.BloodBankName == to.BloodBankName)
                {
                    continue;
                }
                BloodBank bloodBank = bankService.GetByName(to.BloodBankName);
                if (bloodBank == null)
                {
                    continue;
                }
                Email email = new Email(bloodBank.Email,
                    "Tender Offer Result",
                    "TEKST",
                    "We are sorry to inform you that your offer was not accepted.\nThank you for your offer!\nBest regards"
                    );
                emailService.SendEmail(email);
            }
            return StatusCode(201, null);
        }

        [HttpPut("choose")]
        public ActionResult ChooseTender(Tender tender)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (tender == null)
            {
                return BadRequest();
            }

            tender.Status = IntegrationLibrary.Enums.StatusTender.InProcess;
            tenderService.Update(tender);

            BloodBank bloodBank = bankService.GetByName(tender.Winner.BloodBankName);
            if (bloodBank == null)
            {
                return StatusCode(400, null);
            }
            Email email = new Email(bloodBank.Email,
                "Tender Offer Result",
                "TEKST",
                "We are happy to inform you that your offer was accepted.\nClick on this to verificate your offer" 
                + "<a href=\"http://localhost:4200/tender/verification\" > Verificate offer</a>" 
                + "to verificate your offer. In field ID enter <strong>" + tender.Id.ToString() + "</strong> \nBest regards"
                );
            emailService.SendEmail(email);

            return StatusCode(201, null);
        }
        
        [HttpGet("getClosedTenders")]
        public async Task<ActionResult<List<Tender>>> GetClosedTenders()
        {
            var tenders = await tenderService.GetClosedTenders();
            return Ok(tenders);
        }

    }
}