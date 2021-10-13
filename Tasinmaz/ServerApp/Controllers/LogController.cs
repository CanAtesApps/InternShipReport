using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerApp.Data;
using System.Linq;

namespace ServerApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogController:ControllerBase
    {
        private readonly TasinmazContext _context;

        public LogController(TasinmazContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetLog()
        {
            var loglar = await _context.Log.ToListAsync();

            Logger.log(_context,"Butun-Loglari-Al",true);
            await _context.SaveChangesAsync();

            return Ok(loglar);
        }
        [HttpGet("page={pageNumber}filter={search}")]
        public async Task<IActionResult> GetLogTablo(int pageNumber,string search)
        {
            int dataPerPage = 10;
            
            if(string.IsNullOrEmpty(search))
            {
                var loglar = _context.Log
                .Skip((pageNumber-1) * dataPerPage)
                .Take(dataPerPage);

                Logger.log(_context,"Sayfali-loglari-Al",true);
                await _context.SaveChangesAsync();

                return Ok(loglar);
            }
            else
            {
                if(search.ToLower().Equals("basarili") || search.ToLower().Equals("başarılı"))
                {   
                    var loglar = await _context.Log.ToListAsync();
                    var filteredLogs = loglar.Where(item => item.logSuccess)
                    .Skip((pageNumber-1) * dataPerPage)
                    .Take(dataPerPage);

                    Logger.log(_context,"Filtreli-loglari-Al",true);
                    await _context.SaveChangesAsync();

                    return Ok(filteredLogs);
                }
                else if(search.ToLower().Equals("basarisiz") || search.ToLower().Equals("başarısız"))
                {
                    var loglar = await _context.Log.ToListAsync();
                    var filteredLogs = loglar.Where(item => !item.logSuccess)
                    .Skip((pageNumber-1) * dataPerPage)
                    .Take(dataPerPage);

                    Logger.log(_context,"Filtreli-loglari-Al",true);
                    await _context.SaveChangesAsync();

                    return Ok(filteredLogs);
                }
                else
                {
                    var loglar = await _context.Log.ToListAsync();
                    var filteredLogs = loglar.Where(item => 
                    item.logDetails.ToLower().Contains(search.ToLower())
                        || item.logTime.ToString().Contains(search)
                        || item.ipAdress.Contains(search)
                    )
                    .Skip((pageNumber-1) * dataPerPage)
                    .Take(dataPerPage);

                    Logger.log(_context,"Filtreli-loglari-Al",true);
                    await _context.SaveChangesAsync();

                    return Ok(filteredLogs);
                }
            }
        }
        [HttpGet("maxPageNumberFiltreli={search}")]
        public async Task<IActionResult> GetLogCountFiltered(string search)
        {
            if(search.ToLower()==("basarili") || search.ToLower()==("başarılı"))
            {
                var loglar = await _context.Log.ToListAsync();
                var maxItems = loglar.Where(item => item.logSuccess).Count();
                int maxPageNumber = maxItems / 10;
                if(maxItems % 10 != 0)
                {
                    maxPageNumber++;
                }
                return Ok(new {maxPageNumber});
            }
            else if(search.ToLower()==("basarisiz") || search.ToLower()==("başarısız"))
            {

                var loglar = await _context.Log.ToListAsync();
                var maxItems = loglar.Where(item => !item.logSuccess).Count();
                int maxPageNumber = maxItems / 10;
                if(maxItems % 10 != 0)
                {
                    maxPageNumber++;
                }
                return Ok(new {maxPageNumber});
            }
            else
            {
                var loglar =  _context.Log.ToList();
                var maxItems = loglar.Where(item => 
                    item.logDetails.ToLower().Contains(search.ToLower())
                        || item.logTime.ToString().Contains(search)
                        || item.ipAdress.Contains(search)
                ).Count();
                int maxPageNumber = maxItems / 10;
                if(maxItems % 10 != 0)
                {
                    maxPageNumber++;
                }
                return Ok(new {maxPageNumber});
            }
        }
        
        [HttpGet("pageNo={pageNumber}")]
        public async Task<IActionResult> GetLogTabloWithoutFilter(int pageNumber)
        {
            int dataPerPage = 10;
            
                var loglar = _context.Log
                .Skip((pageNumber-1) * dataPerPage)
                .Take(dataPerPage);

                Logger.log(_context,"Sayfali-loglari-Al",true);
                await _context.SaveChangesAsync();

                return Ok(loglar);
        }
        [HttpGet("maxPageNumber")]
        public async Task<IActionResult> GetLogCount()
        {
            var maxItems = await _context.Log
            .CountAsync();
            int maxPageNumber = maxItems / 10;
            if(maxItems % 10 != 0)
            {
                maxPageNumber++;
            }
            return Ok(new {maxPageNumber});
        }
    }
}