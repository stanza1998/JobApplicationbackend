using AutoMapper;
using backend.Core.Context;
using backend.Core.Dtos.Candidate;
using backend.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private ApplicationDbContext _context { get; }

        private IMapper _mapper { get; }
        public CandidateController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        //CRUD
        //CREATE

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult> CreateCandidate([FromForm] CandidateDto dto, IFormFile pdfFile)
        {
            var fiveMegaByte = 5 * 1024 * 1024;
            var pdfMimeType = "application/pdf";

            if(pdfFile.Length > fiveMegaByte || pdfFile.ContentType != pdfMimeType)
            {
                return BadRequest("File is not valid");
            }

            var resumeUrl = Guid.NewGuid().ToString() + ".pdf";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "documents", "pdfs", resumeUrl);
            using (var stream = new FileStream(filePath, FileMode.Create)) 
            {
            await pdfFile.CopyToAsync(stream);
            }

            var newCandidate = _mapper.Map<Candidates>(dto);
            newCandidate.ResumeUrl = resumeUrl;
            await _context.Candidates.AddAsync(newCandidate);
            await _context.SaveChangesAsync();

            return Ok("Candidate Created Successfully");

        }

        //Read
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<CanditateGetDto>>> GetCandidate()
        {
            var candidate = await _context.Candidates.Include(c => c.Job).ToListAsync();
            var convertedCandidates = _mapper.Map<IEnumerable<CanditateGetDto>>(candidate);
            return Ok(convertedCandidates);

        }

        //Download file
        [HttpGet]
        [Route("download/{url}")]

        public IActionResult DownloadPdfFile(string url)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "documents", "pdfs", url);
            if (!System.IO.File.Exists(filePath))
            {
                return BadRequest("File Not Found");
            }

            var pdfBytes = System.IO.File.ReadAllBytes(filePath);
            var file = File(pdfBytes, "application/pdf", url);
            return file;
        }

        //GET BT ID

        //UPDATE

        //DELETE
    }
}
