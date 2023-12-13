using AutoMapper;
using backend.Core.Context;
using backend.Core.Dtos.Company;
using backend.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private ApplicationDbContext _context { get;  }

        private IMapper _mapper { get; }
        public CompanyController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //CRUD

        //Create
        [HttpPost]
        [Route("Create")]

        public async Task<IActionResult> CreateCompany([FromBody] CompanyDto dto)
        {
            Company newCompany = _mapper.Map<Company>(dto);
            await _context.Companies.AddAsync(newCompany);
            await _context.SaveChangesAsync();

            return Ok("New Company Created Successfully");
        }


        //read
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<CompanyGetDto>>> GetCompanies()
        {
            var companies =  await _context.Companies.ToListAsync();
            var convertedCompanies = _mapper.Map<IEnumerable<CompanyGetDto>>(companies);
            return Ok(convertedCompanies);
        }

        // getCompany by Id
        [HttpGet]
        [Route("Get/{id}")]
        public async Task<ActionResult<CompanyGetDto>> GetCompanyById(long id)
        {
            var company = await _context.Companies.FindAsync(id);

            if (company == null)
            {
                return NotFound(); // Return 404 Not Found if the company doesn't exist
            }

            var convertedCompany = _mapper.Map<CompanyGetDto>(company);
            return Ok(convertedCompany);
        }

        //update
        // Assuming you have a DbContext (_context) and IMapper (_mapper) injected into your controller
        // and you have a CompanyUpdateDto that represents the data you want to use for updating.

        [HttpPut]
        [Route("Update/{id}")]
        public async Task<ActionResult> UpdateCompany(long id, [FromBody] CompanyUpdateDto companyUpdateDto)
        {
            if (companyUpdateDto == null)
            {
                return BadRequest("Company data is null.");
            }

            var existingCompany = await _context.Companies.FindAsync(id);

            if (existingCompany == null)
            {
                return NotFound($"Company with ID {id} not found.");
            }

            // You can use AutoMapper here to update the properties of the existingCompany object
            _mapper.Map(companyUpdateDto, existingCompany);

            try
            {
                _context.Companies.Update(existingCompany);
                await _context.SaveChangesAsync();
                return Ok("Company updated successfully.");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during the update process
                return StatusCode(500, $"An error occurred while updating the company: {ex.Message}");
            }
        }


        //delete
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<ActionResult> DeleteCompany(long id)
        {
            var existingCompany = await _context.Companies.FindAsync(id);

            if (existingCompany == null)
            {
                return NotFound($"Company with ID {id} not found.");
            }

            try
            {
                _context.Companies.Remove(existingCompany);
                await _context.SaveChangesAsync();
                return Ok("Company deleted successfully.");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during the delete process
                return StatusCode(500, $"An error occurred while deleting the company: {ex.Message}");
            }
        }
    }
}



