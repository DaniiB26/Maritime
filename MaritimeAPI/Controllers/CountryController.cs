using MaritimeAPI.Data;
using MaritimeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaritimeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public CountryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
            return await _context.Countries.ToListAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Country>> GetCountry(int Id)
        {

            var country = await _context.Countries.FindAsync(Id);

            if (country == null)
            {
                return NotFound();
            }
            return country;
        }

        [HttpPost]
        public async Task<ActionResult<Country>> AddItem(Country country)
        {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCountry), new { id = country.Id }, country);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateCountry(int Id, Country country)
        {
            if (Id == country.Id)
            {
                _context.Entry(country).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCountry(int Id) {
            var country = await _context.Countries.FindAsync(Id);

            if (country == null) {
                return NotFound();
            }

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}