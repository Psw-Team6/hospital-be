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
using IntegrationLibrary.BloodSubscription.Service;
using IntegrationLibrary.BloodSubscription.Model;
using IntegrationLibrary.RabbitMQPublisher;
using IntegrationLibrary.RabbitMQService;
using IntegrationLibrary.RabbitMQService.RabbitMQProducer;

namespace IntegrationAPI.Controllers
{
    /*
    [Route("api/[controller]")]
    [ApiController]
    public class MounthlyBloodSubscriptionController : ControllerBase
    {
        private readonly IBloodSubscriptionService _supService;
        private readonly IBloodBankService _bbService;

        public MounthlyBloodSubscriptionController(IBloodSubscriptionService supService, IBloodBankService bbService)
        {
            _supService = supService;
            _bbService = bbService;
        }

        // POST api/MounthlyBloodSubscription
        /*[HttpPost]
        public ActionResult Create(MounthlyBloodSubscriptionRequest bSup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MounthlyBloodSubscription mounthlyBloodSubscription = new MounthlyBloodSubscription();
            mounthlyBloodSubscription.dateAndTimeOfSubscription = DateTime.Now;
            mounthlyBloodSubscription.bloodBankId = _bbService.GetByName(bSup.bloodBankName).Id;
            mounthlyBloodSubscription.amountOfBloodTypes = bSup.bloodTypeAmountPair;

            IntegrationLibrary.RabbitMQPublisher.MounthlyBloodSubscriptionResponse bSupResponse = new IntegrationLibrary.RabbitMQPublisher.MounthlyBloodSubscriptionResponse();
            bSupResponse.APIKey = _bbService.GetByName(bSup.bloodBankName).ApiKey.Value;
            bSupResponse.dateAndTimeOfSubscription = mounthlyBloodSubscription.dateAndTimeOfSubscription;
            bSupResponse.bloodTypeAmountPair = mounthlyBloodSubscription.amountOfBloodTypes;

            RabbitMQProducer producer = new RabbitMQProducer();
            //producer.SendMessage(bSupResponse);

            RabbitMQPublisher.SendBloodSubscription(bSupResponse);

            _supService.Create(mounthlyBloodSubscription);

            return CreatedAtAction("Create", new { id = mounthlyBloodSubscription.id }, mounthlyBloodSubscription);
        }*/
}