using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerApp.Data;
using ServerApp.Models;

namespace ServerApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IlceController : ControllerBase
    {
        private readonly TasinmazContext _context;

        public IlceController(TasinmazContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetIlce()
        {
            var ilceler = await _context.Ilce.ToListAsync();

            return Ok(ilceler);
        }

        [HttpGet("{cityID}")]
        public async Task<IActionResult> GetIlceByIlCode(int cityID)
        {
            List<Ilce> ilceler = await _context.Ilce.ToListAsync();

            var filtreliIlceler = ilceler
            .Where(p => p.CityID == cityID)
            .Select(p => new
            {
                countyID = p.CountyID,
                countyName = p.CountyName,
                cityID = p.CityID
            });

            if (filtreliIlceler == null)
            {
                return NotFound();
            }

            return Ok(filtreliIlceler);
        }
        [HttpGet("name={cityname}")]
        public async Task<IActionResult> GetIlceByCityName(string cityname)
        {
            cityname = cityname.ToUpper();
            var filtreliIlceler = _context.Ilce
            .Include(x => x.Il)
            .Where(x => x.Il.CityName.Equals(cityname))
            .Select(p => new
            {
                countyID = p.CountyID,
                countyName = p.CountyName,
                cityID = p.CityID
            });

            if (filtreliIlceler == null)
            {
                return NotFound();
            }

            return Ok(filtreliIlceler);
        }

        [HttpGet("getOne={countyname}")]
        public async Task<IActionResult> getOneIlceByName(string countyname)
        {
            var ilce = await _context.Ilce.FirstOrDefaultAsync(i => i.CountyName == countyname);
                
            if (ilce == null)
            {
                return NotFound();
            }

            return Ok(ilce);
        }
        
        [HttpGet("id={id}getOne={countyname}")]
        public async Task<IActionResult> getOneIlceByCityId(int id,string countyname)
        {
            List<Ilce> ilceler = await _context.Ilce.ToListAsync();
            countyname = countyname.ToUpper();
            var wantedIlce = ilceler
            .Where(p => p.CityID == id)
            .Select(p => new
            {
                countyID = p.CountyID,
                countyName = p.CountyName,
                cityID = p.CityID
            }).FirstOrDefault(x => x.countyName.Equals(countyname));

            return Ok(wantedIlce);
            
        }
    }
}