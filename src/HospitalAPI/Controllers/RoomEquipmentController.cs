using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Dtos.Response;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Rooms.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Private
{
    [Route("api/v1/[controller]")]
    [ApiController]
    
    
    public class RoomEquipmentController:ControllerBase
    {
        
        private readonly EquipmentService _equipmentService;
        private readonly IMapper _mapper;

        public RoomEquipmentController(EquipmentService equipmentService, IMapper mapper)
        {
            _equipmentService = equipmentService;
            _mapper = mapper;
        }
        
        
        // GET: api/rooms
        [HttpGet]
        [ProducesResponseType(typeof(List<RoomEquipmentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<RoomEquipmentResponse>>> GetAllEquipment()
        {
            var equipments = await _equipmentService.GetAllEquipment();
            var result = _mapper.Map<List<RoomEquipmentResponse>>(equipments);
            return Ok(result);
        }
        
        
        
        [HttpGet("getAllEquipmentByRoomId/{roomId}")]
        [ProducesResponseType(typeof(List<RoomEquipmentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<RoomEquipmentResponse>>> GetAllEquipmentByRoomId([FromRoute]Guid roomId)
        {
            var result = await _equipmentService.GetAllEquipmentByRoomId(roomId);
            return result == null ? NotFound() : Ok(result);
        }
        
        
        
        
        [HttpGet("SearchEquipmentByName/{equipmentName}")]
        [ProducesResponseType(typeof(List<RoomEquipmentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<RoomEquipmentResponse>>> SearchEquipmentByName([FromRoute]string equipmentName) //Vamo stavi string Eq
        {
          // string equipmentName = Eq.Trim().ToLower();
            var result = await _equipmentService.SearchEquipmentByName(equipmentName);
            return result == null ? NotFound() : Ok(result);
        }
        
        
        
        
        
        [HttpGet("{roomEquipmentId}")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RoomEquipmentResponse>> AllEquipmentById(Guid roomEquipmentId)
        {
            var equipments = await _equipmentService.AllEquipmentById(roomEquipmentId);
            var result = _mapper.Map<RoomEquipmentResponse>(equipments);
            return equipments == null ? NotFound() : Ok(result);

        }
        
    }
}