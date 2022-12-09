using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _service;
        private readonly IConfiguration _config;

        public LoginController(ILoginService service, IConfiguration config)
        {
            _service = service;
            _config = config;
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.UserName) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return BadRequest("Missing login details");
            }

            var loginResponse = await _service.LoginAsync(loginRequest);

            if (loginResponse == null)
            {
                return BadRequest($"Invalid credentials");
            }
            return Ok(loginResponse);
        }


    }
}
