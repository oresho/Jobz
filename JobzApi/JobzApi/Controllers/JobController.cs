using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobzApi.Data;
using JobzApi.Models.Entities;
using JobzApi.Services.Interfaces;
using JobzApi.Models.Dtos.Requests;

namespace JobzApi.Controllers
{
    [Route("api/v1/jobs")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpGet]
        public async Task<IActionResult> GetJobs([FromQuery] int pageNo = 1, [FromQuery] int pageSize = 3)
        {
            var reponse = await _jobService.GetAllJobsAsync(pageNo, pageSize);
            return Ok(reponse);
        }

        [HttpGet("{id}")] 
        public async Task<ActionResult<Job>> GetJob(int id)
        {
            var response = await _jobService.GetJobAsync(id);
            return Ok(response);
        }

        //// PUT: api/Jobs/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutJob(int id, Job job)
        //{
        //    if (id != job.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(job).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!JobExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Jobs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> AddNewJob([FromBody] CreateJobRequest jobRequest)
        {
            var response = await _jobService.CreateJobAsync(jobRequest);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var response = await _jobService.DeleteJobAsync(id);
            return Ok(response);
        }
    }
}
