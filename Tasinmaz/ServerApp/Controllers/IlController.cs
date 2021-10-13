using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerApp.Data;


namespace ServerApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IlController:ControllerBase
    {
        private readonly TasinmazContext _context;

        public IlController(TasinmazContext context )
        {
            _context =  context;
        }

        [HttpGet]
        public async Task<IActionResult> GetIls()
        {
            var iller = await _context.Il.Select(x=> new {
                    cityID = x.CityID,
                    cityName=x.CityName
            }).ToListAsync();
            return Ok(iller);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetIlById(int id)
        {
            var il = await _context.Il.FirstOrDefaultAsync(i => i.CityID == id);
           
            if (il == null)
            {
                return NotFound();
            }

            return Ok(il);
        }
        [HttpGet("name={name}")]
        public async Task<ActionResult> GetIlById(string name)
        {
            var il = await _context.Il.FirstOrDefaultAsync(i => i.CityName == name);
           
            if (il == null)
            {
                return NotFound();
            }

            return Ok(il);
        }
    }
}