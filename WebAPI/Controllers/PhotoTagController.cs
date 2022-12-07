using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoTagController : ControllerBase
    {
        private readonly IPhotoTagService _service;

        public PhotoTagController(IPhotoTagService service)
        {
            _service = service;
        }

        // GET: api/<PhotoTagController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhotoTagModel>>> Get()
        {
            var photoTags = await _service.GetAllAsync();

            if (photoTags == null)
            {
                return NotFound();
            }
            else
            {

                return new ObjectResult(photoTags);
            }

        }

        // GET api/<PhotoTagController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PhotoTagModel>> GetById(int id)
        {
            var photoTag = await _service.GetByIdAsync(id);
            if (photoTag == null)
            {
                return NotFound();
            }
            else
            {
                return new ObjectResult(photoTag);
            }
        }

        // POST api/AlbumPhoto
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PhotoTagModel photoTag)
        {
            if (photoTag == null)
            {
                return BadRequest();
            }
            try
            {
                await _service.AddAsync(photoTag);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(photoTag);

        }

        // PUT api/<PhotoTagController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] PhotoTagModel value)
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

        // DELETE api/<PhotoTagController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var photoTag = await _service.GetByIdAsync(id);
            if (photoTag == null)
            {
                return NotFound();
            }
            await _service.DeleteAsync(id);
            return Ok(photoTag);
        }
    }
}
