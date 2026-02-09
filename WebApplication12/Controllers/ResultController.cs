using Microsoft.AspNetCore.Mvc;
using WebApplication12.Data;

namespace WebApplication12.Controllers
{
    [ApiController]
    [Route("api/result")]
    public class ResultController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ResultController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{userId}")]
        public IActionResult GetResult(int userId)
        {
            try
            {
                var result = _context.Reports
                    .Where(r => r.UserId == userId)
                    .OrderByDescending(r => r.ReportId)
                    .FirstOrDefault();

                if (result == null)
                    return NotFound(new { message = "No result found" });

                return Ok(result);
            }
            catch
            {
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }
    }
}
