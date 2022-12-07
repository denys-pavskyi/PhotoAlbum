using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Services;
using DataAccessLayer.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumPhotoController : ControllerBase
    {
        private readonly IAlbumPhotoService _service;

        public AlbumPhotoController(IAlbumPhotoService service)
        {
            _service = service;
        }


        // GET: api/AlbumPhoto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlbumPhotoModel>>> Get()
        {
            var albumPhotos = await _service.GetAllAsync();

            if (albumPhotos == null)
            {
                return NotFound();
            }
            else
            {

                return new ObjectResult(albumPhotos);
            }
            
        }

        // GET api/AlbumPhoto/5
        [HttpGet("{id}")]
        public string GetAlbumPhoto(int id)
        {
            return "value";
        }

        // POST api/AlbumPhoto
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AlbumPhotoModel AlbumPhoto)
        {
            if (AlbumPhoto == null)
            {
                return BadRequest();
            }   
            try
            {
                await _service.AddAsync(AlbumPhoto);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(AlbumPhoto);

        }

        // PUT api/AlbumPhoto/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/AlbumPhoto/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
