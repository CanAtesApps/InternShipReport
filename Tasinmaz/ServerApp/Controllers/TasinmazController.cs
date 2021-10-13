using System;
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
    public class TasinmazController:ControllerBase
    {
        private readonly TasinmazContext _context;
        public TasinmazController(TasinmazContext context )
        {
            _context =  context;
        }
        
        [HttpGet("tablo")]
        public async Task<IActionResult> GetTasinmazTablo()
        {
            var lambdaInclude = _context.Tasinmaz
            .Include(x => x.Mahalle)
            .Include(x => x.Mahalle.Ilce)
            .Include(x => x.Mahalle.Ilce.Il)
            .Select(x => new {
                tasinmazID  = x.TasinmazID,
                cityName    = x.Mahalle.Ilce.Il.CityName,
                countyName  = x.Mahalle.Ilce.CountyName,
                areaName    = x.Mahalle.AreaName,
                ada         = x.Ada,
                parsel      = x.Parsel,
                nitelik     = x.Nitelik,
                adres       = x.Adres,
                isActive    = x.isActive,
            });
            Logger.log(_context,"Butun-Tasinmazlari-Al",true);
            await _context.SaveChangesAsync();

            var activeTasinmaz = lambdaInclude.Where(x => x.isActive);
            return Ok(activeTasinmaz);
        }

        [HttpGet("page={pageNumber}")]
        public async Task<IActionResult> GetTasinmazTablo(int pageNumber)
        {

            int dataPerPage = 5;
            var activeTasinmaz = _context.Tasinmaz
            .Include(x => x.Mahalle)
            .Include(x => x.Mahalle.Ilce)
            .Include(x => x.Mahalle.Ilce.Il)
            .Select(x => new {
                tasinmazID  = x.TasinmazID,
                cityName    = x.Mahalle.Ilce.Il.CityName,
                countyName  = x.Mahalle.Ilce.CountyName,
                areaName    = x.Mahalle.AreaName,
                ada         = x.Ada,
                parsel      = x.Parsel,
                nitelik     = x.Nitelik,
                adres       = x.Adres,
                isActive    = x.isActive,
            })
            .Where(x => x.isActive)
            .Skip((pageNumber-1) * dataPerPage)
            .Take(dataPerPage);

            Logger.log(_context,"Butun-Tasinmazlari-Al",true);
            await _context.SaveChangesAsync();

            return Ok(activeTasinmaz);
        }

        [HttpGet("pageNo={pageNumber}filter={filter}")]
        public async Task<IActionResult> GetTasinmazTabloWithFilter(int pageNumber,string filter)
        {
            int dataPerPage = 5;
            var activeTasinmaz = _context.Tasinmaz
            .Include(x => x.Mahalle)
            .Include(x => x.Mahalle.Ilce)
            .Include(x => x.Mahalle.Ilce.Il)
            .Select(x => new {
                tasinmazID  = x.TasinmazID,
                cityName    = x.Mahalle.Ilce.Il.CityName,
                countyName  = x.Mahalle.Ilce.CountyName,
                areaName    = x.Mahalle.AreaName,
                ada         = x.Ada,
                parsel      = x.Parsel,
                nitelik     = x.Nitelik,
                adres       = x.Adres,
                isActive    = x.isActive,
            })
            .Where(x => x.isActive)
            .Where(x => x.cityName.ToLower().Contains(filter.ToLower())
                || x.countyName.ToLower().Contains(filter.ToLower())
                || x.areaName.ToLower().Contains(filter.ToLower())
                || x.adres.ToLower().Contains(filter.ToLower())
                || x.ada.ToString().Contains(filter)
                || x.parsel.ToString().Contains(filter)
                || x.nitelik.ToLower().Contains(filter.ToLower())
            )
            .Skip((pageNumber-1) * dataPerPage)
            .Take(dataPerPage);

            Logger.log(_context,"Butun-Tasinmazlari-Al",true);
            await _context.SaveChangesAsync();

            return Ok(activeTasinmaz);
        }
        [HttpGet("getIDs={id}")]
        public async Task<IActionResult>getIds(int id)
        {
            var Ids = await _context.Tasinmaz
            .Include(x => x.Mahalle)
            .Include(x => x.Mahalle.Ilce)
            .Include(x => x.Mahalle.Ilce.Il)
            .FirstOrDefaultAsync(x=> x.TasinmazID == id);

            var response =  new {
                cityID = Ids.CityID,
                countyID = Ids.MahalleID,
                areaID = Ids.MahalleID
            };

            return Ok(Ids.CityID);
        }
        [HttpGet("maxPageNumber")]
        public async Task<IActionResult> GetTasinmazCount()
        {
            var maxItems = await _context.Tasinmaz
            .Include(x => x.Mahalle)
            .Include(x => x.Mahalle.Ilce)
            .Include(x => x.Mahalle.Ilce.Il)
            .Select(x => new {
                tasinmazID  = x.TasinmazID,
                cityName    = x.Mahalle.Ilce.Il.CityName,
                countyName  = x.Mahalle.Ilce.CountyName,
                areaName    = x.Mahalle.AreaName,
                ada         = x.Ada,
                parsel      = x.Parsel,
                nitelik     = x.Nitelik,
                adres       = x.Adres,
                isActive    = x.isActive,
            })
            .Where(x => x.isActive)
            .CountAsync();
            int maxPageNumber = maxItems / 5;
            if(maxItems % 5 != 0)
            {
                maxPageNumber++;
            }
            return Ok(new {maxPageNumber});
        }
        
        [HttpGet("maxPageNumberFiltreli={filter}")]
        public async Task<IActionResult> GetTasinmazMaxPageNumberWithFilter(string filter)
        {
            var maxItems = await _context.Tasinmaz
            .Include(x => x.Mahalle)
            .Include(x => x.Mahalle.Ilce)
            .Include(x => x.Mahalle.Ilce.Il)
            .Select(x => new {
                tasinmazID  = x.TasinmazID,
                cityName    = x.Mahalle.Ilce.Il.CityName,
                countyName  = x.Mahalle.Ilce.CountyName,
                areaName    = x.Mahalle.AreaName,
                ada         = x.Ada,
                parsel      = x.Parsel,
                nitelik     = x.Nitelik,
                adres       = x.Adres,
                isActive    = x.isActive,
            })
            .Where(x => x.isActive)
            .Where(x => x.cityName.ToLower().Contains(filter.ToLower())
                || x.countyName.ToLower().Contains(filter.ToLower())
                || x.areaName.ToLower().Contains(filter.ToLower())
                || x.adres.ToLower().Contains(filter.ToLower())
                || x.ada.ToString().Contains(filter)
                || x.parsel.ToString().Contains(filter)
                || x.nitelik.ToLower().Contains(filter.ToLower())
            )
            .CountAsync();
            int maxPageNumber = maxItems / 5;
            if(maxItems % 5 != 0)
            {
                maxPageNumber++;
            }
            return Ok(new {maxPageNumber});
        }
        [HttpGet]
        public async Task<IActionResult> GetTasinmaz()
        {
            var tasinmazlar = await _context.Tasinmaz.Where(x => x.isActive).ToListAsync();

            Logger.log(_context,"Butun-Tasinmazlari-Al",true);
            await _context.SaveChangesAsync();

            return Ok(tasinmazlar);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTasinmazById(int id)
        {
            var tmp = await _context.Tasinmaz.FindAsync(id);

            if(tmp == null || !tmp.isActive)
            {
                Logger.log(_context,"Tasinmaz-Al-Bulunamadi",false);
                await _context.SaveChangesAsync();

                return NotFound();
            }
            else
            {
                Logger.log(_context,"Tasinmaz-Al",true);
                await _context.SaveChangesAsync();
                
                return Ok(tmp);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTasinmaz(Tasinmaz entity)
        {
            entity.isActive = true;
            var tmp = findAndReturn(entity);
            if(tmp == null || !tmp.isActive)
            {
                _context.Tasinmaz.Add(entity);
                Logger.log(_context,"Tasinmaz-Ekle",true);
                await _context.SaveChangesAsync();
               
                return CreatedAtAction(nameof(GetTasinmaz), new {id = entity.TasinmazID}, entity);
            }

            else
            {
                Logger.log(_context,"Tasinmaz-Ekle-Hatali-Istek",false);
                await _context.SaveChangesAsync();
                
                return BadRequest();
            }
        }
        private  Tasinmaz findAndReturn(Tasinmaz entity)
        {
            return _context.Tasinmaz.FirstOrDefault(x =>x.CityID == entity.CityID 
                                                                    && x.CountyID  == entity.CountyID
                                                                    && x.MahalleID == entity.MahalleID
                                                                    && x.Ada       == entity.Ada
                                                                    && x.Parsel    == entity.Parsel
                                                                    && x.Nitelik   == entity.Nitelik
                                                                    && x.Adres     == entity.Adres);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTasinmaz(int id, Tasinmaz entity)
        {
            if(id != entity.TasinmazID)
            {
                Logger.log(_context,"Tasinmaz-Guncelle-Hatali-istek",false);
                await _context.SaveChangesAsync();
                return BadRequest();
            }

            var tmp = await _context.Tasinmaz.FindAsync(id);

            if(tmp == null)
            {
                Logger.log(_context,"Tasinmaz-Guncelle-Bulunamadi",false);
                await _context.SaveChangesAsync();
                return NotFound();
            }

            updateProperties(tmp, entity);
            Logger.log(_context,"Tasinmaz-Guncelle",true);
            await _context.SaveChangesAsync();
          
            return NoContent();
        }
        private void updateProperties(Tasinmaz tmp, Tasinmaz entity)
        {
            tmp.CityID    = entity.CityID;
            tmp.CountyID  = entity.CountyID;
            tmp.MahalleID = entity.MahalleID;
            tmp.Ada       = entity.Ada;
            tmp.Parsel    = entity.Parsel;
            tmp.Nitelik   = entity.Nitelik;
            tmp.Adres     = entity.Adres;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTasinmaz(int id)
        {
            var tmp = await _context.Tasinmaz.FindAsync(id);
            
            if(tmp == null)
            {
                Logger.log(_context,"Tasinmaz-Sil-Bulunamadi",false);
                await _context.SaveChangesAsync();
                return NotFound();
            }

            tmp.isActive = false;
            
            Logger.log(_context,"Tasinmaz-Sil",true);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }
    }
}