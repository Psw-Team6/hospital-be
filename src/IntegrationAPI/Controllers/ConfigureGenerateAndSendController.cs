using AutoMapper;
using IntegrationLibrary.ConfigureGenerateAndSend.Model;
using IntegrationLibrary.ConfigureGenerateAndSend.Service;
using Microsoft.AspNetCore.Mvc;
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


        // POST api/configureGenerateAndSend
        [HttpPost]
        public ActionResult Create(ConfigureGenerateAndSend configureGenerateAndSend)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var configure = _mapper.Map<ConfigureGenerateAndSend>(configureGenerateAndSend);
            _configureGenerateAndSendService.Create(configureGenerateAndSend);
            return CreatedAtAction("GetById", new { id = configureGenerateAndSend.BloodBankName }, configureGenerateAndSend);
        }

      


    }
}
