using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _service;

        public AlbumController(IAlbumService service)
        {
            _service = service;
        }

        // GET: api/<AlbumController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlbumModel>>> Get()
        {
            var albums = await _service.GetAllAsync();

            if (albums == null)
            {
                return NotFound();
            }
            else
            {

                return new ObjectResult(albums);
            }

        }

        // GET api/<AlbumController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlbumModel>> GetById(int id)
        {
            var album = await _service.GetByIdAsync(id);
            if (album == null)
            {
                return NotFound();
            }
            else
            {
                return new ObjectResult(album);
            }
        }

        // POST api/AlbumPhoto
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AlbumModel album)
        {
            if (album == null)
            {
                return BadRequest();
            }
            try
            {
                await _service.AddAsync(album);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(album);

        }

        // PUT api/<AlbumController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] AlbumModel value)
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

        // DELETE api/<AlbumController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var album = await _service.GetByIdAsync(id);
            if (album == null)
            {
                return NotFound();
            }
            await _service.DeleteAsync(id);
            return Ok(album);
        }
    }
}
