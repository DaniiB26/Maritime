using MaritimeAPI.Data;
using MaritimeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaritimeAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class VoyageController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        public VoyageController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Voyage>>> GetVoyages()
        {
            return await _context.Voyages
                            .Include(v => v.DeparturePort)
                                .ThenInclude(p => p.Country)
                            .Include(v => v.ArrivalPort)
                                .ThenInclude(p => p.Country)
                            .Include(v => v.Ship)
                            .ToListAsync();

        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Voyage>> GetVoyage(int Id)
        {

            var voyage = await _context.Voyages.FindAsync(Id);

            if (voyage == null)
            {
                return NotFound();
            }
            return voyage;
        }

        [HttpPost]
        public async Task<ActionResult<Voyage>> AddItem(Voyage voyage)
        {
            _context.Voyages.Add(voyage);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetVoyage), new { id = voyage.Id }, voyage);
        }


        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateVoyage(int Id, Voyage voyage)
        {
            if (Id == voyage.Id)
            {
                _context.Entry(voyage).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteVoyage(int Id)
        {
            var voyage = await _context.Voyages.FindAsync(Id);

            if (voyage == null)
            {
                return NotFound();
            }

            _context.Voyages.Remove(voyage);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}