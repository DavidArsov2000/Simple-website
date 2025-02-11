using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.DatabaseContext;
using Backend.Models;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class PersonsController : ControllerBase 
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<PersonsController> _logger;

        public PersonsController(ApplicationDbContext context, ILogger<PersonsController> logger)
        {
            _context = context;
            _logger = logger;
        }


        [HttpGet("allPersons")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Oseba>>> Get()
        {
            try
            {
                var osebe = await _context.Osebe.ToListAsync();
                if (osebe.Count == 0)
                {       
                    return NotFound();
                }
                return Ok(osebe.OrderBy(x => x.Ime));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get failed: \n{ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpPost("addPerson")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult<Oseba>> Post(Oseba oseba)
        {
            try
            {
                _context.Add(oseba);
                await _context.SaveChangesAsync();

                // Praviloma post vrne ustvarjeni objekt, vendar navodila zahtevajo vračilo vseh objektov
                var osebe = await _context.Osebe.ToListAsync();
                return Ok(osebe.OrderBy(x => x.Ime));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Adding failed: \n{ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
    }
}
