using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService _service;

        public PhotoController(IPhotoService service)
        {
            _service = service;
        }

        // GET: api/photos
        [HttpGet]
        //[Route("photos")]
        //[Authorize(Policy = "OnlyNonBannedUser")]
        public async Task<ActionResult<IEnumerable<PhotoModel>>> Get()
        {
            


            var photos = await _service.GetAllAsync();

            if (photos == null)
            {
                return NotFound();
            }
            else
            {

                return new ObjectResult(photos);
            }

        }

        // GET api/<PhotoController>/5
        [HttpGet("{id}")]
        //[Route("photo/{id}")]
        public async Task<ActionResult<PhotoModel>> GetById(int id)
        {
            var photo = await _service.GetByIdAsync(id);
            if (photo == null)
            {
                return NotFound();
            }
            else
            {
                return new ObjectResult(photo);
            }
        }

        // POST api/AlbumPhoto
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PhotoModel photo)
        {

            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null)
            {

                return Unauthorized("Invalid user");
            }

            if (photo == null)
            {
                return BadRequest();
            }
            try
            {
                await _service.AddAsync(photo);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(photo);

        }

        // PUT api/<PhotoController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] PhotoModel value)
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

        // DELETE api/<PhotoController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var photo = await _service.GetByIdAsync(id);
            if (photo == null)
            {
                return NotFound();
            }
            await _service.DeleteAsync(id);
            return Ok(photo);
        }
    }
}
