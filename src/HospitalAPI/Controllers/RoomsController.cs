using System;
using AutoMapper;
using HospitalAPI.Dtos.Request;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;

        public RoomsController(IRoomService roomService, IMapper mapper)
        {
            _roomService = roomService;
            _mapper = mapper;
        }

        // GET: api/rooms
        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_roomService.GetAll());
        }

        // GET api/rooms/2
        [HttpGet("{id}")]
        public ActionResult GetById(Guid id)
        {
            var room = _roomService.GetById(id);
            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }

        // POST api/rooms
        [HttpPost]
        public ActionResult Create(RoomRequest roomRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var room = _mapper.Map<Room>(roomRequest);
            _roomService.Create(room);
            return CreatedAtAction("GetById", new { id = room.Id }, room);
        }

        // PUT api/rooms/2
        [HttpPut("{id}")]
        public ActionResult Update(Guid id, Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != room.Id)
            {
                return BadRequest();
            }

            try
            {
                _roomService.Update(room);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(room);
        }

        // DELETE api/rooms/2
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var room = _roomService.GetById(id);
            if (room == null)
            {
                return NotFound();
            }

            _roomService.Delete(room);
            return NoContent();
        }
        [HttpGet("/time")]
        public string Time()
        {
            return DateTime.Now.TimeOfDay.ToString();
        }
        
    }
}
