using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChatbotAPI.Data;
using ChatbotAPI.Models;

namespace ChatbotAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatsonController : ControllerBase
    {
        private readonly ChatbotContext _context;

        public WatsonController(ChatbotContext context)
        {
            _context = context;
        }

        // GET: api/Watson
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WatsonPayload>>> GetWatsonPayload()
        {
            return await _context.WatsonPayload.ToListAsync();
        }

        // GET: api/Watson/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WatsonPayload>> GetWatsonPayload(int id)
        {
            var watsonPayload = await _context.WatsonPayload.FindAsync(id);

            if (watsonPayload == null)
            {
                return NotFound();
            }

            return watsonPayload;
        }

        // PUT: api/Watson/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWatsonPayload(int id, WatsonPayload watsonPayload)
        {
            if (id != watsonPayload.id)
            {
                return BadRequest();
            }

            _context.Entry(watsonPayload).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WatsonPayloadExists(id))
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

        // POST: api/Watson
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<WatsonPayload>> PostWatsonPayload(WatsonPayload watsonPayload)
        {
            _context.WatsonPayload.Add(watsonPayload);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWatsonPayload", new { id = watsonPayload.id }, watsonPayload);
        }

        // DELETE: api/Watson/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WatsonPayload>> DeleteWatsonPayload(int id)
        {
            var watsonPayload = await _context.WatsonPayload.FindAsync(id);
            if (watsonPayload == null)
            {
                return NotFound();
            }

            _context.WatsonPayload.Remove(watsonPayload);
            await _context.SaveChangesAsync();

            return watsonPayload;
        }

        private bool WatsonPayloadExists(int id)
        {
            return _context.WatsonPayload.Any(e => e.id == id);
        }
    }
}
