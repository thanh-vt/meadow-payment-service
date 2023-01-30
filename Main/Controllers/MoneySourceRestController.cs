using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeadowPaymentService.Data;
using MeadowPaymentService.Models;
using Microsoft.AspNetCore.Authorization;

namespace MeadowPaymentService.Controllers
{
    [Authorize]
    [Route("/api/money-source")]
    [ApiController]
    public class MoneySourceRestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MoneySourceRestController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/money-source
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MoneySource>>> GetMoneySource()
        {
            return await _context.MoneySource.ToListAsync();
        }

        // GET: api/money-source/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MoneySource>> GetMoneySource(string id)
        {
            var moneySource = await _context.MoneySource.FindAsync(id);

            if (moneySource == null)
            {
                return NotFound();
            }

            return moneySource;
        }

        // PUT: api/money-source/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{code}")]
        public async Task<IActionResult> PutMoneySource(string code, MoneySource moneySource)
        {
            if (code != moneySource.Code)
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
                if (!MoneySourceExists(code))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/money-source
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MoneySource>> PostMoneySource(MoneySource moneySource)
        {
            _context.MoneySource.Add(moneySource);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMoneySource", new { id = moneySource.Code }, moneySource);
        }

        // DELETE: api/money-source/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMoneySource(string id)
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

        private bool MoneySourceExists(string code)
        {
            return _context.MoneySource.Any(e => e.Code == code);
        }
    }
}
