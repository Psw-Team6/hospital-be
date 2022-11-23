using AutoMapper;
using IntegrationAPI.Dtos.Request;
using IntegrationLibrary.ConfigureGenerateAndSend.Model;
using IntegrationLibrary.ConfigureGenerateAndSend.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace IntegrationAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ConfigureGenerateAndSendController : ControllerBase
    {
        private readonly IConfigureGenerateAndSendService _configureGenerateAndSendService;
        private readonly IMapper _mapper;

        public ConfigureGenerateAndSendController(IConfigureGenerateAndSendService configureGenerateAndSendService, IMapper mapper)
        {
            _configureGenerateAndSendService = configureGenerateAndSendService;
            _mapper = mapper;

        }

        // GET api/v1/configureGenerateAndSend
        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_configureGenerateAndSendService.GetAll());
        }

        // POST api/v1/configureGenerateAndSend
        [HttpPost]
        public ActionResult Create(ConfigureGenerateAndSend configureGenerateAndSend)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var configure = _mapper.Map<ConfigureGenerateAndSend>(configureGenerateAndSend);
           _configureGenerateAndSendService.Create(configureGenerateAndSend);
            return Ok();
        }

        [HttpPost("edit")]
        public ActionResult Edit(ConfigureGenerateAndSend configureGenerateAndSend)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                List<ConfigureGenerateAndSend> configurations = (List<ConfigureGenerateAndSend>)_configureGenerateAndSendService.GetAll();
                for(int i=0; i < configurations.Count; i++)
                {
                    if (configureGenerateAndSend.BloodBankName.Equals(configurations[i].BloodBankName))
                    {
                        _configureGenerateAndSendService.Delete(configurations[i]);
                    }
                }

                var configure = _mapper.Map<ConfigureGenerateAndSend>(configureGenerateAndSend);
                _configureGenerateAndSendService.Edit(configureGenerateAndSend);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(configureGenerateAndSend);
        }




    }
}
