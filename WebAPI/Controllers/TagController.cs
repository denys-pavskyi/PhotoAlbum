using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Validation;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {




        private readonly ITagService _service;

        public TagController(ITagService service)
        {
            _service = service;
        }

        // GET: api/<TagController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagModel>>> Get()
        {
            var tags = await _service.GetAllAsync();

            if (tags == null)
            {
                return NotFound();
            }
            else
            {

                return new ObjectResult(tags);
            }

        }

        // GET api/<TagController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TagModel>> GetById(int id)
        {
            var tag = await _service.GetByIdAsync(id);
            if (tag == null)
            {
                return NotFound();
            }
            else
            {
                return new ObjectResult(tag);
            }
        }

        // POST api/AlbumPhoto
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TagModel tag)
        {
            if (tag == null)
            {
                return BadRequest();
            }
            try
            {
                await _service.AddAsync(tag);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(tag);

        }

        // PUT api/<TagController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] TagModel value)
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

        // DELETE api/<TagController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var tag = await _service.GetByIdAsync(id);
            if(tag == null)
            {
                return NotFound();
            }
            await _service.DeleteAsync(id);
            return Ok(tag);
        }
    }
}
