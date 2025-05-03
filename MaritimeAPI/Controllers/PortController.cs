using Microsoft.AspNetCore.Mvc;
using MaritimeAPI.Data;
using MaritimeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MaritimeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PortController : ControllerBase {

        private readonly ApplicationDbContext _context;

        public PortController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Port>>> GetPorts()
        {
            return await _context.Ports
                            .Include(p => p.Country)
                            .ToListAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Port>> GetPort(int Id)
        {

            var port = await _context.Ports.FindAsync(Id);

            if (port == null)
            {
                return NotFound();
            }
            return port;
        }

        [HttpPost]
        public async Task<ActionResult<Port>> AddItem(Port port)
        {
            _context.Ports.Add(port);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPort), new { id = port.Id }, port);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdatePort(int Id, Port port)
        {
            if (Id == port.Id)
            {
                _context.Entry(port).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletePort(int Id) {
            var port = await _context.Ports.FindAsync(Id);

            if (port == null) {
                return NotFound();
            }

            _context.Ports.Remove(port);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}