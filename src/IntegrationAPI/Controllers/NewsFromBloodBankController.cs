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
using IntegrationLibrary.NewsFromBloodBank.Service;
using System.Collections.Generic;
using IntegrationLibrary.NewsFromBloodBank.Model;

namespace IntegrationAPI.Controllers
{
    [Route("api/news")]
    [ApiController]
    public class NewsFromBloodBankController : ControllerBase
    {
        private readonly INewsFromBloodBankService _newsService;
        private readonly IMapper _mapper;

        public NewsFromBloodBankController(INewsFromBloodBankService bloodBankService, IMapper mapper)
        {
            _newsService = bloodBankService;
            _mapper = mapper;

        }

        [HttpGet]
        public ActionResult GetAllOnHold()
        {
            return Ok(_newsService.GetAllOnHold());
        }

        [HttpPut("{id}")]
        public ActionResult Update(Guid id, NewsFromBloodBank newsFromBloodBank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != newsFromBloodBank.Id)
            {
                return BadRequest();
            }

            try
            {
                _newsService.Update(newsFromBloodBank);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(newsFromBloodBank);
        }
    }
}
