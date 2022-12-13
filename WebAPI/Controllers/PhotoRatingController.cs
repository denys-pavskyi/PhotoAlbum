using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class PhotoRatingController : ControllerBase
    {
        private readonly IPhotoRatingService _service;

        public PhotoRatingController(IPhotoRatingService service)
        {
            _service = service;
        }

        // GET: api/<PhotoRatingController>
        [HttpGet("photoRatings")]
        public async Task<ActionResult<IEnumerable<PhotoRatingModel>>> Get()
        {
            var photoRatings = await _service.GetAllAsync();

            if (photoRatings == null)
            {
                return NotFound();
            }
            else
            {

                return new ObjectResult(photoRatings);
            }

        }

        // GET api/<PhotoRatingController>/5
        [HttpGet("photoRating/{id}")]
        public async Task<ActionResult<PhotoRatingModel>> GetById(int id)
        {
            var photoRating = await _service.GetByIdAsync(id);
            if (photoRating == null)
            {
                return NotFound();
            }
            else
            {
                return new ObjectResult(photoRating);
            }
        }

        // POST api/AlbumPhoto
        [HttpPost("photoRating")]
        public async Task<ActionResult> Post([FromBody] PhotoRatingModel photoRating)
        {
            if (photoRating == null)
            {
                return BadRequest();
            }
            try
            {
                await _service.AddAsync(photoRating);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(photoRating);

        }

        // PUT api/<PhotoRatingController>/5
        [HttpPut("photoRating/{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] PhotoRatingModel value)
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

        // DELETE api/<PhotoRatingController>/5
        [HttpDelete("photoRating/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var photoRating = await _service.GetByIdAsync(id);
            if (photoRating == null)
            {
                return NotFound();
            }
            await _service.DeleteAsync(id);
            return Ok(photoRating);
        }
    }
}
