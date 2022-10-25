using System.Threading.Tasks;
using AutoMapper;
using HospitalLibrary.Appointments.Service;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly AppointmentService _appointmentService;
        private readonly IMapper _mapper;

        public AppointmentController(AppointmentService appointmentService, IMapper mapper)
        {
            _appointmentService = appointmentService;
            _mapper = mapper;
        }

        // public async Task<ActionResult> GetAllAppointments()
        // {
        //     return await 
        // }
    }
}