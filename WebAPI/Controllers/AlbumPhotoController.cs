using AutoMapper;
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
        private readonly AlbumPhotoService _albumPhotoService;

        public AlbumPhotoController(InternetPhotoAlbumDbContext context , IMapper mapper)
        {
            UnitOfWork uow = new UnitOfWork(context);
            _albumPhotoService = new AlbumPhotoService(uow, mapper);
        }


        // GET: api/AlbumPhoto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlbumPhotoModel>>> GetAlbumPhotos()
        {
            var albumPhotos = await _albumPhotoService.GetAllAsync();

            if (albumPhotos == null)
            {
                return NotFound();
            }
            else
            {

                return new ObjectResult(albumPhotos);
            }
            ;
        }

        // GET api/AlbumPhoto/5
        [HttpGet("{id}")]
        public string GetAlbumPhoto(int id)
        {
            return "value";
        }

        // POST api/AlbumPhoto
        [HttpPost]
        public async Task<ActionResult> PostAlbumPhoto([FromBody] AlbumPhotoModel albumPhoto)
        {
            if (albumPhoto == null)
            {
                return BadRequest();
            }   
            try
            {
                await _albumPhotoService.AddAsync(albumPhoto);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(albumPhoto);

        }

        // PUT api/AlbumPhoto/5
        [HttpPut("{id}")]
        public void PutAlbumPhoto(int id, [FromBody] string value)
        {
        }

        // DELETE api/AlbumPhoto/5
        [HttpDelete("{id}")]
        public void DeleteAlbumPhoto(int id)
        {
        }
    }
}
