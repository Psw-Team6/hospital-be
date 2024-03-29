﻿using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalAPI.Infrastructure.Authorization;
using HospitalLibrary.ApplicationUsers.Model;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Rooms.Service;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [HospitalAuthorization(UserRole.Manager)]
    public class GRoomsController:ControllerBase
    {
        private readonly GRoomService _roomService;

        public GRoomsController(GRoomService roomService)
        {
            _roomService = roomService;
        }
        [HttpGet]
        public async Task<ActionResult<List<GRoom>>> GetAll()
        {
            var rooms = await  _roomService.GetAllGRooms();
            return Ok(rooms);
        }
    }
}