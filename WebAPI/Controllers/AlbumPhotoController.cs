﻿using AutoMapper;
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

        // GET: api/<AlbumPhotoController>
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

        // GET api/<AlbumPhotoController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlbumPhotoModel>> GetById(int id)
        {
            var albumPhoto = await _service.GetByIdAsync(id);
            if (albumPhoto == null)
            {
                return NotFound();
            }
            else
            {
                return new ObjectResult(albumPhoto);
            }
        }

        // POST api/AlbumPhoto
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AlbumPhotoModel albumPhoto)
        {
            if (albumPhoto == null)
            {
                return BadRequest();
            }
            try
            {
                await _service.AddAsync(albumPhoto);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(albumPhoto);

        }

        // PUT api/<AlbumPhotoController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] AlbumPhotoModel value)
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

        // DELETE api/<AlbumPhotoController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var albumPhoto = await _service.GetByIdAsync(id);
            if (albumPhoto == null)
            {
                return NotFound();
            }
            await _service.DeleteAsync(id);
            return Ok(albumPhoto);
        }
    }
}
