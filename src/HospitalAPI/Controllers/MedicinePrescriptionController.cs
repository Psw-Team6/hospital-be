using System;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Dtos.Request;
using HospitalAPI.Dtos.Response;
using HospitalLibrary.Prescriptions.Model;
using HospitalLibrary.Prescriptions.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MedicinePrescriptionController : ControllerBase
    {
        private readonly MedicinePrescriptionService _medicinePrescriptionService;
        private readonly IMapper _mapper;
        
        public MedicinePrescriptionController(MedicinePrescriptionService medicinePrescriptionService, IMapper mapper)
        {
            _medicinePrescriptionService = medicinePrescriptionService;
            _mapper = mapper;
        }
    }
}