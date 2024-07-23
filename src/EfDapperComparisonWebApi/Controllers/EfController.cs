using Microsoft.AspNetCore.Mvc;

namespace EfDapperComparison.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EfController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EfController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("insert")]
        public async Task<IActionResult> Insert()
        {
            var record = new TestRecord { CreatedDate = DateTime.UtcNow };
            _context.TestRecords.Add(record);
            await _context.SaveChangesAsync();
            return Ok(record);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id)
        {
            var record = _context.TestRecords.FirstOrDefault(r => r.Id == id);
            if (record == null)
            {
                return NotFound();
            }

            record.CreatedDate = DateTime.UtcNow;
            _context.TestRecords.Update(record);
            await _context.SaveChangesAsync();
            return Ok(record);
        }
    }
}
