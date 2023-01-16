using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Dtos.Request;
using HospitalAPI.Dtos.Response;
using HospitalAPI.Infrastructure.Authorization;
using HospitalLibrary.ApplicationUsers.Model;
using HospitalLibrary.Feedbacks.Model;
using HospitalLibrary.Feedbacks.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly FeedbackService _feedbackService;
        private readonly IMapper _mapper;

        public FeedbackController(FeedbackService feedbackService, IMapper mapper)
        {
            _feedbackService = feedbackService;
            _mapper = mapper;
        }

        // GET: api/rooms
        [HttpGet]
        [HospitalAuthorization(UserRole.Manager)]
        public async Task<ActionResult<IEnumerable<FeedbackResponse>>> GetAll()
        {
            var feedbacks = await _feedbackService.GetAll();
            var result = _mapper.Map<IEnumerable<FeedbackResponse>>(feedbacks);
            return Ok(result);
        }
        
        [HttpGet("/api/v1/feedback-public")]
        public async Task<ActionResult<IEnumerable<FeedbackResponse>>> GetAllPublic()
        {
            var feedbacks = await _feedbackService.GetAllPublic();
            var result = _mapper.Map<IEnumerable<FeedbackResponse>>(feedbacks);
            return Ok(result);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HospitalAuthorization(UserRole.Patient)]
        public async Task<ActionResult<FeedbackResponse>> CreateFeedback([FromBody] FeedbackRequest feedbackRequest)
        {
            var feedback = _mapper.Map<Feedback>(feedbackRequest);
            var result = await _feedbackService.CreateFeedback(feedback);
            return CreatedAtAction(nameof(GetById), new {id = result.Id}, result);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FeedbackResponse>> GetById([FromRoute] Guid id)
        {
            var feedback = await _feedbackService.GetById(id);
            var result = _mapper.Map<FeedbackResponse>(feedback);
            return result == null ? NotFound() : Ok(result);
        }
       
        [HttpPut]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HospitalAuthorization(UserRole.Manager)]
        public async Task<ActionResult> UpdateFeedbackStatus([FromBody] FeedbackStatusResponse feedbackStatusResponse)
        {
            var feedback = _mapper.Map<Feedback>(feedbackStatusResponse);
            var result = await _feedbackService.Update(feedback);
            return result == false ? NotFound() : NoContent();
        }
    }
}
