using MaritimeAPI.Data;
using MaritimeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaritimeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShipController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public ShipController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ship>>> GetShips()
        {
            return await _context.Ships.ToListAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Ship>> GetShip(int Id)
        {

            var ship = await _context.Ships.FindAsync(Id);

            if (ship == null)
            {
                return NotFound();
            }
            return ship;
        }

         [HttpPost]
        public async Task<ActionResult<Ship>> AddItem(Ship ship)
        {
            _context.Ships.Add(ship);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetShip), new { id = ship.Id }, ship);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateShip(int Id, Ship ship)
        {
            if (Id == ship.Id)
            {
                _context.Entry(ship).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteShip(int Id) {
            var ship = await _context.Ships.FindAsync(Id);

            if (ship == null) {
                return NotFound();
            }

            _context.Ships.Remove(ship);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}