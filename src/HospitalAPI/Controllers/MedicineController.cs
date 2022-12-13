using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Dtos.Response;
using HospitalLibrary.Medicines.Model;
using HospitalLibrary.Medicines.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly MedicineService _medicineService;
        private readonly IMapper _mapper;
        
        public MedicineController(MedicineService medicineService, IMapper mapper)
        {
            _medicineService = medicineService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Medicine>>> GetAll()
        {
            var medicines = await _medicineService.GetAllByPatientId();
            var result = _mapper.Map<List<Medicine>>(medicines);
            return Ok(result);
        }
        [HttpGet("Examination")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<MedicineExaminationResponse>>> GetAllForExamination()
        {
            var medicines = await _medicineService.GetAllByPatientId();
            var result = _mapper.Map<List<MedicineExaminationResponse>>(medicines);
            return Ok(result);
        }
    }
}