using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlannerAPI.Data;
using PlannerAPI.Model;

namespace PlannerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemosController : ControllerBase
    {
        private readonly PlannerAPIContext _context;

        public MemosController(PlannerAPIContext context)
        {
            _context = context;
        }

        // GET: api/Memos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Memo>>> GetMemo()
        {
          if (_context.Memo == null)
          {
              return NotFound();
          }
            return await _context.Memo.ToListAsync();
        }

        // GET: api/Memos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Memo>> GetMemo(string id)
        {
          if (_context.Memo == null)
          {
              return NotFound();
          }
            var memo = await _context.Memo.FindAsync(id);

            if (memo == null)
            {
                return NotFound();
            }

            return memo;
        }

        // PUT: api/Memos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMemo(string id, Memo memo)
        {
            if (id != memo.MemoId)
            {
                return BadRequest();
            }

            _context.Entry(memo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemoExists(id))
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

        // POST: api/Memos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Memo>> PostMemo(Memo memo)
        {
          if (_context.Memo == null)
          {
              return Problem("Entity set 'PlannerAPIContext.Memo'  is null.");
          }
            _context.Memo.Add(memo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MemoExists(memo.MemoId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

           // return CreatedAtAction("GetMemo", new { id = memo.MemoId }, memo);
            return CreatedAtAction(nameof(GetMemo), new { id = memo.MemoId }, memo);
        }

        // DELETE: api/Memos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMemo(string id)
        {
            if (_context.Memo == null)
            {
                return NotFound();
            }
            var memo = await _context.Memo.FindAsync(id);
            if (memo == null)
            {
                return NotFound();
            }

            _context.Memo.Remove(memo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MemoExists(string id)
        {
            return (_context.Memo?.Any(e => e.MemoId == id)).GetValueOrDefault();
        }
    }
}
