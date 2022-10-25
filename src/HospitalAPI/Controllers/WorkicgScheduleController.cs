using AutoMapper;
using HospitalLibrary.Doctors.Service;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    public class WorkicgScheduleController : ControllerBase
    {
        private readonly WorkingScheduleService _workingScheduleService;
        private readonly IMapper _mapper;
        
        public WorkicgScheduleController( WorkingScheduleService workingScheduleService, IMapper mapper)
        {
            _workingScheduleService = workingScheduleService;
            _mapper = mapper;
        }
    }
}