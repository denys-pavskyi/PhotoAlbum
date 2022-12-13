using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _service;

        public ReportController(IReportService service)
        {
            _service = service;
        }

        // GET: api/<ReportController>
        [HttpGet("reports")]
        public async Task<ActionResult<IEnumerable<ReportModel>>> Get()
        {
            var reports = await _service.GetAllAsync();

            if (reports == null)
            {
                return NotFound();
            }
            else
            {

                return new ObjectResult(reports);
            }

        }

        // GET: api/<ReportController>
        [HttpGet("reports/onReview")]
        public async Task<ActionResult<IEnumerable<ReportModel>>> GetReportsOnReview()
        {
            var reports = await _service.GetReportsOnReview();

            if (reports == null)
            {
                return NotFound();
            }
            else
            {

                return new ObjectResult(reports);
            }

        }

        // GET api/<ReportController>/5
        [HttpGet("report/{id}")]
        public async Task<ActionResult<ReportModel>> GetById(int id)
        {
            var report = await _service.GetByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            else
            {
                return new ObjectResult(report);
            }
        }

        // POST api/AlbumPhoto
        [HttpPost("report")]
        public async Task<ActionResult> Post([FromBody] ReportModel report)
        {
            if (report == null)
            {
                return BadRequest();
            }
            try
            {
                await _service.AddAsync(report);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(report);

        }

        // PUT api/<ReportController>/5
        [HttpPut("report/{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ReportModel value)
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

        // DELETE api/<ReportController>/5
        [HttpDelete("report/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var report = await _service.GetByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            await _service.DeleteAsync(id);
            return Ok(report);
        }
    }
}
