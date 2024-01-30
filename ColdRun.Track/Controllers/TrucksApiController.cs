using ColdRun.Truck.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ColdRun.Truck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrucksApiController : ControllerBase
    {
        private readonly TruckDbContext _context;

        public TrucksApiController(TruckDbContext context)
        {
            _context = context;
        }

        // GET: api/TrucksApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Domain.Truck>>> GetTrucks()
        {
          if (_context.Trucks == null)
          {
              return NotFound();
          }
            return await _context.Trucks.ToListAsync();
        }

        // GET: api/TrucksApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Domain.Truck>> GetTruck(Guid id)
        {
          if (_context.Trucks == null)
          {
              return NotFound();
          }
            var truck = await _context.Trucks.FindAsync(id);

            if (truck == null)
            {
                return NotFound();
            }

            return truck;
        }

        // PUT: api/TrucksApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTruck(Guid id, Domain.Truck truck)
        {
            if (id != truck.Id)
            {
                return BadRequest();
            }

            _context.Entry(truck).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TruckExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TrucksApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Domain.Truck>> PostTruck(Domain.Truck truck)
        {
          if (_context.Trucks == null)
          {
              return Problem("Entity set 'TruckDbContext.Trucks'  is null.");
          }
            _context.Trucks.Add(truck);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTruck", new { id = truck.Id }, truck);
        }

        // DELETE: api/TrucksApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTruck(Guid id)
        {
            if (_context.Trucks == null)
            {
                return NotFound();
            }
            var truck = await _context.Trucks.FindAsync(id);
            if (truck == null)
            {
                return NotFound();
            }

            _context.Trucks.Remove(truck);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TruckExists(Guid id)
        {
            return (_context.Trucks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
