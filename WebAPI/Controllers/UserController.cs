﻿using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        // GET: api/<UserController>
        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<UserModel>>> Get()
        {
            var users = await _service.GetAllAsync();

            if (users == null)
            {
                return NotFound();
            }
            else
            {

                return new ObjectResult(users);
            }

        }

        // GET api/<UserController>/5
        [HttpGet("user/{id}")]
        public async Task<ActionResult<UserModel>> GetById(int id)
        {
            var user = await _service.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return new ObjectResult(user);
            }
        }

        // POST api/AlbumPhoto
        [HttpPost("user")]
        public async Task<ActionResult> Post([FromBody] UserModel user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            try
            {
                await _service.AddAsync(user);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(user);

        }

        // PUT api/<UserController>/5
        [HttpPut("user/{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UserModel value)
        {
            try
            {
                await _service.UpdateAsync(value);
            }
            catch
            {
                return BadRequest();
            }


            if (await _service.GetByIdAsync(id) == null)
            {
                return BadRequest();
            }

            return Ok(value);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("user/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await _service.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            await _service.DeleteAsync(id);
            return Ok(user);
        }
    }
}
