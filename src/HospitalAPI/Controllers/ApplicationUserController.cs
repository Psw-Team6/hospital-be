using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using HospitalAPI.Dtos.Request;
using HospitalAPI.Dtos.Response;
using HospitalLibrary.ApplicationUsers.Model;
using HospitalLibrary.ApplicationUsers.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using HospitalAPI.Exceptions;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace HospitalAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ApplicationUserController:ControllerBase
    {
        private readonly ApplicationUserService _userService;
        private readonly IHttpClientFactory _clientFactory;
        private const string StaffUrl = "https://doctor-portal";
        private const string PatientUrl = "https://patient-portal";
        private const string BloodBankUrl = "http://localhost:5001";

        public ApplicationUserController(ApplicationUserService userService, IHttpClientFactory clientFactory)
        {
            _userService = userService;
            _clientFactory = clientFactory;
        }
        [HttpPost("Authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LoginResponse>> Authenticate([FromBody]LoginRequest loginRequest)
        {
            var jwtBloodBank = await AuthenticateBloodBank(loginRequest);
            if (jwtBloodBank != "")
            {
                return Ok(new LoginResponse
                {
                    Token = jwtBloodBank,
                    Message = "Login Success"
                });   
            }
            var user = await _userService.Authenticate(loginRequest.Username, loginRequest.Password);
           if (user.UserRole is UserRole.Patient)
           {
               if (loginRequest.PortalUrl != PatientUrl)
               {
                   return BadRequest(new ResponseContent()
                   {
                       Error = "Bad Credentials!"
                   });
               }
               if (user.IsBlocked)
               {
                   return BadRequest(new ResponseContent()
                   {
                       Error = "User blocked!"
                   });
               }
           }
           if (user.UserRole is UserRole.Doctor or UserRole.Manager)
           {
               if (loginRequest.PortalUrl != StaffUrl)
               {
                   return BadRequest(new ResponseContent()
                   {
                       Error = "Bad Credentials!"
                   });
               }
           }
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

        private async Task<string> AuthenticateBloodBank(LoginRequest loginRequest)
        {
            var token = "";
            try
            {
                var bank = new BankRequest
                {
                    Name = loginRequest.Username,
                    Password = loginRequest.Password
                };
                var client = _clientFactory.CreateClient();
                client.BaseAddress = new Uri(BloodBankUrl);
                var response = await client.PostAsJsonAsync("/api/BloodBank/Authenticate", bank);
                if (!response.IsSuccessStatusCode) return token;
                var content = await response.Content.ReadAsStringAsync();
                var bankLoginResponse = JsonConvert.DeserializeObject<BankLoginResponse>(content);
                token = CreateBloodBankJwt(bankLoginResponse);
            }
            catch (Exception ex)
            {
                token = "";
            }

            return token;
        }


        private static string CreateJwt(ApplicationUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("veryverysecret.....");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Role, user.UserRole.ToString()),
                new(ClaimTypes.Name,$"{user.Username}"),
                new(ClaimTypes.NameIdentifier,$"{user.Id}")
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
        private static string CreateBloodBankJwt(BankLoginResponse bank)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("veryverysecret.....");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Role, UserRole.BloodBank.ToString()),
                new(ClaimTypes.Name,$"{bank.Name}"),
                new(ClaimTypes.NameIdentifier,$"{bank.Id}")
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