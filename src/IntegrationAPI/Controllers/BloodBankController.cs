﻿using AutoMapper;
using IntegrationAPI.Dtos.Request;
using IntegrationLibrary.BloodBank;
using IntegrationLibrary.BloodBank.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Security;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Collections.Generic;
using IntegrationLibrary.HTTP;
using IntegrationLibrary.BloodRequests.Model;
using IntegrationAPI.Dtos.Response;

namespace IntegrationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodBankController: ControllerBase
    {
        private readonly IBloodBankService _bloodBankService;
        private readonly IHttpService _httpService;
        private readonly IMapper _mapper;

        public BloodBankController(IBloodBankService bloodBankService, IMapper mapper, IHttpService httpService)
        {
            _bloodBankService = bloodBankService;
            _mapper = mapper;
            _httpService = httpService;
        }

        // GET: api/bloodbank
        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_bloodBankService.GetAll());
        }

        [HttpGet("bloodBankName",Name = "Get")]
        public List<BloodBankName> GetBloodBankName()

        {
            List<BloodBank> BloodBanks = (List<BloodBank>)_bloodBankService.GetAll();
            List<BloodBankName> bloodBankNames = new List<BloodBankName>();
            for(int i=0; i< BloodBanks.Count; i++)
            {
                bloodBankNames.Add(new BloodBankName((BloodBanks[i].Name)));
            }

            return bloodBankNames;
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
        [HttpPost("Authenticate")]
        public ActionResult Authenticate([FromBody] LoginRequest loginRequest)
        {
            var bank = _bloodBankService.Authenticate(loginRequest.Name, loginRequest.Password);
            if (bank == null)
            {
                return BadRequest();
            }

            return Ok(new LoginResponse
            {
                Name = bank.Name,
                Id = bank.Id
            });
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

        // GET api/bloodbank/BankaKrvi
        [HttpGet("findByAPIKey/{ApiKey}")]
        public ActionResult GetByAPIKey(String ApiKey)
        {
            var bloodBank = _bloodBankService.GetByAPIKey(ApiKey);
            if (bloodBank == null)
            {
                return Ok(null);
            }

            return Ok(bloodBank);
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
        public async Task<IntegrationLibrary.BloodRequests.Model.BloodSupplyResponse> GetBBSupplyByTypeAndQuantity(String bloodType, String quantity)
        {
            return await _httpService.GetProductAsync("http://localhost:8080/api/blood/bloodType/" + bloodType +'/'+ quantity);
        }

    }
}
