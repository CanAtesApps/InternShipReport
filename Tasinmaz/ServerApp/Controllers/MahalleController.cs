using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerApp.Data;

namespace ServerApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MahalleController:ControllerBase
    {
        private readonly TasinmazContext _context;

        public MahalleController(TasinmazContext context )
        {
            _context =  context;
        }
       
        [HttpGet]
        public async Task<ActionResult> GetMahalle()
        {
            var mahalleler = await _context.Mahalle.ToListAsync();
           
            return Ok(mahalleler);
        }

        [HttpGet("{countyID}")]
        public async Task<ActionResult> GetIlceByIlCode(int countyID)
        {
            var filtreliMahalleler = await _context.Mahalle
            .Where(p => p.CountyID == countyID)
            .Select(p => new {
                mahalleID = p.AreaID,
                mahalleName = p.AreaName,
                countyID =p.CountyID
            })
           .ToListAsync();
           
            if(filtreliMahalleler == null)
            {
                return NotFound();
            }

            return Ok(filtreliMahalleler);
        }
        [HttpGet("getOne={areaname}")]
        public async Task<IActionResult> getOneIlceByName(string areaname)
        {
            var mahalle = await _context.Mahalle.FirstOrDefaultAsync(i => i.AreaName == areaname);
                
            if (mahalle == null)
            {
                return NotFound();
            }

            return Ok(mahalle);
        }
         [HttpGet("id={id}getOne={areaname}")]
        public async Task<IActionResult> getOneMahalleByCityId(int id,string areaname)
        {
            areaname = areaname.ToUpper();

            var wantedArea = await _context.Mahalle
            .Where(p => p.CountyID == id)
            .Select(p => new {
                mahalleID = p.AreaID,
                mahalleName = p.AreaName,
                countyID =p.CountyID
            }).FirstOrDefaultAsync(x=> x.mahalleName.Equals(areaname));

            return Ok(wantedArea);
        }
    }
}