using AutoMapper;
using HospitalLibrary.Doctors.Service;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    public class WorkingScheduleController : ControllerBase
    {
        private readonly WorkingScheduleService _workingScheduleService;
        private readonly IMapper _mapper;
        public WorkingScheduleController( WorkingScheduleService workingScheduleService, IMapper mapper)
        {
            _workingScheduleService = workingScheduleService;
            _mapper = mapper;
        }
    }
}