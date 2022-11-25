using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MLSZ.Data;
using MLSZ.Entities;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MLSZ.Controllers
{
    [Authorize(Roles = "Admin,Owner")]
    [ApiController]
    [Route("api/[controller]")]
    public class FieldController : ControllerBase
    {
        private readonly MlszContext _context;
        public FieldController(MlszContext context)
        {
            _context = context;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult<Field[]>> Index()
        {
            return _context.Fields.ToArray();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Field>> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var palya = await _context.Fields
                .FirstOrDefaultAsync(m => m.Id == id);
            if (palya == null)
            {
                return NotFound();
            }

            return palya;
        }

        // POST api/<ValuesController>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Field palya)
        {
            if (ModelState.IsValid)
            {
                _context.Add(palya);
                await _context.SaveChangesAsync();
                return RedirectToAction("GetAll");
            }
            return Ok(palya);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] Field palya)
        {
            if (id != palya.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(palya);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PalyaExists(palya.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
            }
            return Ok(palya);
        }

        // DELETE api/<ValuesController>/5
        [HttpPost("delete/{id}"), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var palya = await _context.Fields.FindAsync(id);
            _context.Fields.Remove(palya);
            await _context.SaveChangesAsync();
            return RedirectToAction("GetAll");
        }

        private bool PalyaExists(int id)
        {
            return _context.Fields.Any(e => e.Id == id);
        }
    }
}
