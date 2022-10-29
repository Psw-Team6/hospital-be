﻿using IntegrationLibrary.BloodBank;
using IntegrationLibrary.BloodBank.Service;
using Microsoft.AspNetCore.Mvc;
using System;

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
    }
}