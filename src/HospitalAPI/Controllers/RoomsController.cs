using System;
using System.Threading.Tasks;
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
        private readonly RoomService _roomService;
        private readonly IMapper _mapper;

        public RoomsController(RoomService roomService, IMapper mapper)
        {
            _roomService = roomService;
            _mapper = mapper;
        }

        // GET: api/rooms
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _roomService.GetAll());
        }

        // GET api/rooms/2
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var room = await _roomService.GetById(id);

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
        [HttpPut("{id:guid}")]
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
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var room = await _roomService.GetById(id);

            _roomService.Delete(room);
            return NoContent();
        }
    }
}
