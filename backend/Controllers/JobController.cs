using AutoMapper;
using backend.Core.Context;
using backend.Core.Dtos.Company;
using backend.Core.Dtos.Job;
using backend.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private ApplicationDbContext _context { get; }

        private IMapper _mapper { get; }
        public JobController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //CRUD

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateJob([FromBody] JobCreateDto dto)
        {
            var newJob = _mapper.Map<Job>(dto);
            await _context.Jobs.AddAsync(newJob);
            await _context.SaveChangesAsync();

            return Ok("New Job Was Created");
        }

    
        //read
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<JobGetDto>>> GetJobs()
        {
            var jobs = await _context.Jobs.Include(job => job.Company).ToListAsync();
            var jobsConverted = _mapper.Map<IEnumerable<JobGetDto>>(jobs);

            return Ok(jobsConverted);
        }


        //read by job Id very important
/*        [HttpGet]
        [Route("Get/{id}")]
        public async Task<ActionResult<JobGetDto>> GetJobById(long id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if(job == null)
            {
                return NotFound();
            }

            var convertedJob = _mapper.Map<JobGetDto>(job);
            
            return Ok(convertedJob);

        }*/



        //update
        [HttpPut]
        [Route("Update/{id}")]
       public async Task<ActionResult> UpdateJob(long id, [FromBody] JobUpdateDto jobUpdateDto)
        {
            if (jobUpdateDto == null)
            {
                return BadRequest("Job data is null.");
            }

            var existingJob = await _context.Jobs.FindAsync(id);

            if (existingJob == null)
            {
                return NotFound($"Job with ID {id} not found.");
            }

            // You can use AutoMapper here to update the properties of the existing job object
            _mapper.Map(jobUpdateDto, existingJob);

            try
            {
                _context.Jobs.Update(existingJob);
                await _context.SaveChangesAsync();
                return Ok("Job updated successfully.");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during the update process
                return StatusCode(500, $"An error occurred while updating the JOB: {ex.Message}");
            }
        }



        //delete
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<ActionResult> DeleteJob(long id)
        {
            var existingJob = await _context.Jobs.FindAsync(id);

            if (existingJob == null)
            {
                return NotFound($"Job with ID {id} not found.");
            }

            try
            {
                _context.Jobs.Remove(existingJob);
                await _context.SaveChangesAsync();
                return Ok("Job deleted successfully.");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during the delete process
                return StatusCode(500, $"An error occurred while deleting the company: {ex.Message}");
            }
        }
    }
}
