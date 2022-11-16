using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using AutoMapper;
using HospitalAPI.Dtos.Request;
using HospitalAPI.Dtos.Response;
using HospitalLibrary.ApplicationUsers.Model;
using HospitalLibrary.ApplicationUsers.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using YamlDotNet.Core.Tokens;

namespace HospitalAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ApplicationUserController:ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApplicationUserService _userService;

        public ApplicationUserController(IMapper mapper, ApplicationUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }
        
        [HttpPost("Authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LoginResponse>> Authenticate([FromBody]LoginRequest loginRequest)
        {
           var user = await _userService.Authenticate(loginRequest.Username, loginRequest.Password);
           var token = CreateJwt(user);
           return Ok(new LoginResponse
           {
               Token = token,
               Message = "Login Success"
           });
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ApplicationUser>>> GetAllApplicationUsers()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }



        private static string CreateJwt(ApplicationUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("veryverysecret.....");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Role, user.UserRole.ToString()),
                new(ClaimTypes.Name,$"{user.Username}"),
                new(ClaimTypes.Email,$"{user.Email}")
            });
            var credential = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credential
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}