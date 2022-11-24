using System;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Dtos.Request;
using HospitalAPI.Dtos.Response;
using HospitalLibrary.TreatmentReports.Model;
using HospitalLibrary.TreatmentReports.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TreatmentReportController : ControllerBase
    {
        private readonly TreatmentReportService _treatmentReportService;
        private readonly IMapper _mapper;
        
        public TreatmentReportController( TreatmentReportService treatmentReportService, IMapper mapper)
        {
            _treatmentReportService = treatmentReportService;
            _mapper = mapper;
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TreatmentReportIdResponse>> GetById([FromRoute] Guid id)
        {
            var patient = await _treatmentReportService.GetByPatientAdmissionId(id);
            var result = _mapper.Map<TreatmentReportIdResponse>(patient);
            return result == null ? NotFound() : Ok(result);
        }
        
    }
}