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
using System.Linq;

namespace IntegrationAPI.Controllers
{
    [Route("api/news")]
    [ApiController]
    public class NewsFromBloodBankController : ControllerBase
    {
        private readonly INewsFromBloodBankService _newsService;

        public NewsFromBloodBankController(INewsFromBloodBankService service)
        {
            _newsService = service;
        }
        [HttpGet("get-first")]
        public NewsFromBloodBank GetFirst() {
            return _newsService.GetAllOnHold().First();
        }

        [HttpGet]
        public ActionResult GetAllOnHold()
        {
            IEnumerable<NewsFromBloodBank> news = _newsService.GetAllOnHold();
            return Ok(news);
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
