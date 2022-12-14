using AutoMapper;
using IntegrationAPI.Dtos.Request;
using IntegrationLibrary.ConfigureGenerateAndSend.Model;
using IntegrationLibrary.ConfigureGenerateAndSend.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace IntegrationAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ConfigureGenerateAndSendController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IConfigureGenerateAndSendService _configureGenerateAndSendService;

        public ConfigureGenerateAndSendController(IConfigureGenerateAndSendService configureGenerateAndSendService, IMapper mapper)
        {
            _configureGenerateAndSendService = configureGenerateAndSendService;
            _mapper = mapper;
            
        }


        [HttpGet("get-first")]
        public ConfigureGenerateAndSend GetFirst()
        {
            return _configureGenerateAndSendService.GetAll().First();
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

            if (configureGenerateAndSend == null)
                return BadRequest();

            if (_configureGenerateAndSendService.IsNameEqual(configureGenerateAndSend))
                return BadRequest();
            
            try
            {
                var configure = _mapper.Map<ConfigureGenerateAndSend>(configureGenerateAndSend);
                _configureGenerateAndSendService.Create(configureGenerateAndSend);
            }
            catch
            {
                return BadRequest();
            }

            return new OkObjectResult(configureGenerateAndSend);

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
                List<ConfigureGenerateAndSend> configurations = _configureGenerateAndSendService.GetAll().ToList();
                for(int i=0; i < configurations.Count; i++)
                {
                    if (_configureGenerateAndSendService.IsNameEqual(configureGenerateAndSend))
                        _configureGenerateAndSendService.Delete(configurations[i]); 
                }

                var configure = _mapper.Map<ConfigureGenerateAndSend>(configureGenerateAndSend);
                _configureGenerateAndSendService.Edit(configureGenerateAndSend);
            }
            catch
            {
                return BadRequest();
            }

            return new  OkObjectResult(configureGenerateAndSend);
        }


        /*public bool IsNameEqual(ConfigureGenerateAndSend configureGenerateAndSend)
        {
            List<ConfigureGenerateAndSend> configurations = _configureGenerateAndSendService.GetAll().ToList();

            for (int i = 0; i < configurations.Count; i++)
            {
                if (configureGenerateAndSend.BloodBankName.Equals(configurations[i].BloodBankName))
                    return true;
            }

            return false;
        }*/

    }
}
