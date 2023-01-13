using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeadowPaymentService.Data;
using MeadowPaymentService.Models;

namespace MeadowPaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoneySourceRestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MoneySourceRestController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MoneySourceRest
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MoneySource>>> GetMoneySource()
        {
            return await _context.MoneySource.ToListAsync();
        }

        // GET: api/MoneySourceRest/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MoneySource>> GetMoneySource(int id)
        {
            var moneySource = await _context.MoneySource.FindAsync(id);

            if (moneySource == null)
            {
                return NotFound();
            }

            return moneySource;
        }

        // PUT: api/MoneySourceRest/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMoneySource(int id, MoneySource moneySource)
        {
            if (id != moneySource.Id)
            {
                return BadRequest();
            }

            _context.Entry(moneySource).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MoneySourceExists(id))
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

        // POST: api/MoneySourceRest
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MoneySource>> PostMoneySource(MoneySource moneySource)
        {
            _context.MoneySource.Add(moneySource);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMoneySource", new { id = moneySource.Id }, moneySource);
        }

        // DELETE: api/MoneySourceRest/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMoneySource(int id)
        {
            var moneySource = await _context.MoneySource.FindAsync(id);
            if (moneySource == null)
            {
                return NotFound();
            }

            _context.MoneySource.Remove(moneySource);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MoneySourceExists(int id)
        {
            return _context.MoneySource.Any(e => e.Id == id);
        }
    }
}
