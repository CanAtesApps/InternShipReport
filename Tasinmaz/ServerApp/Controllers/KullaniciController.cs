using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerApp.Data;
using ServerApp.Models;

namespace ServerApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KullaniciController:ControllerBase
    {
        private readonly TasinmazContext _context;

        public KullaniciController(TasinmazContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetKullanici()
        {
            var kullanicilar = await _context.Kullanici.Where(x => x.isActive).Select(x => new {
                userID = x.UserID,
                ad = x.Ad,
                soyad = x.Soyad,
                email = x.Email,
                rol = x.Rol ? "admin" : "kullanici",
            })
            .ToListAsync();
           
            Logger.log(_context,"Butun-Kullanicilari-Al",true);
            await _context.SaveChangesAsync();

            return Ok(kullanicilar);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetKullaniciId(int id)
        {
            var tmp = await _context.Kullanici.FindAsync(id);

            if(tmp == null || !tmp.isActive)
            {
                Logger.log(_context,"Kullanici-Al-Bulunamadi",false);
                await _context.SaveChangesAsync();
                return NotFound();
            }
            else
            {
                Logger.log(_context,"Kullanici-Al",true);
                await _context.SaveChangesAsync();
                return Ok(tmp);
            }
        }
        [HttpGet("page={pageNumber}filter={search}")]
        public async Task<IActionResult> GetUserTablo(int pageNumber,string search)
        {
            int dataPerPage = 10;
            
            if(string.IsNullOrEmpty(search))
            {
                var kullanicilar = _context.Kullanici.Where(x => x.isActive).Select(x => new {
                userID = x.UserID,
                ad = x.Ad,
                soyad = x.Soyad,
                email = x.Email,
                rol = x.Rol ? "admin" : "kullanici",
                }).Skip((pageNumber-1) * dataPerPage)
                .Take(dataPerPage);

                Logger.log(_context,"Sayfali-loglari-Al",true);
                await _context.SaveChangesAsync();

                return Ok(kullanicilar);
            }
            else
            {
                var kullanicilar = await _context.Kullanici.Where(x => x.isActive).Select(x => new {
                    userID = x.UserID,
                    ad = x.Ad,
                    soyad = x.Soyad,
                    email = x.Email,
                    rol = x.Rol ? "admin" : "kullanici",
                }).ToListAsync();
                var filteredKullanicilar = kullanicilar.Where(item => 
                    item.ad.ToLower().Contains(search.ToLower())
                        || item.soyad.ToLower().Contains(search.ToLower())
                        || item.email.ToLower().Contains(search.ToLower())
                        || item.rol.ToLower().Contains(search.ToLower())
                ).Skip((pageNumber-1) * dataPerPage)
                .Take(dataPerPage);

                Logger.log(_context,"Filtreli-loglari-Al",true);
                await _context.SaveChangesAsync();

                return Ok(filteredKullanicilar);
            }
        }
        [HttpGet("pageNo={pageNumber}")]
        public async Task<IActionResult> GetUserTabloWithoutFilter(int pageNumber)
        {
            int dataPerPage = 10;
            var kullanicilar = _context.Kullanici.Where(x => x.isActive).Select(x => new {
                userID = x.UserID,
                ad = x.Ad,
                soyad = x.Soyad,
                email = x.Email,
                rol = x.Rol ? "admin" : "kullanici",
                }).Skip((pageNumber-1) * dataPerPage)
                .Take(dataPerPage);

                Logger.log(_context,"Sayfali-loglari-Al",true);
                await _context.SaveChangesAsync();

                return Ok(kullanicilar);
        }
        [HttpGet("maxPageNumber")]
        public async Task<IActionResult> GetUserCount()
        {
           var maxItems = await _context.Kullanici.Where(x => x.isActive).CountAsync();
                int maxPageNumber = maxItems / 10;
                if(maxItems % 10 != 0)
                {
                    maxPageNumber++;
                }
                return Ok(new {maxPageNumber});
        }

        [HttpGet("maxPageNumberFiltreli={search}")]
        public async Task<IActionResult> GetUserCountFiltered(string search)
        {
        
            if(string.IsNullOrEmpty(search))
            {
                var maxItems = await _context.Kullanici.Where(x => x.isActive).CountAsync();
                int maxPageNumber = maxItems / 10;
                if(maxItems % 10 != 0)
                {
                    maxPageNumber++;
                }
                return Ok(new {maxPageNumber});
            }
            else
            {
                var kullanicilar = await _context.Kullanici.Where(x => x.isActive).Select(x => new {
                    userID = x.UserID,
                    ad = x.Ad,
                    soyad = x.Soyad,
                    email = x.Email,
                    rol = x.Rol ? "admin" : "kullanici",
                }).ToListAsync();
                var maxItems = kullanicilar.Where(item => 
                    item.ad.ToLower().Contains(search.ToLower())
                        || item.soyad.ToLower().Contains(search.ToLower())
                        || item.email.ToLower().Contains(search.ToLower())
                        || item.rol.ToLower().Contains(search.ToLower())
                    ).Count();
                int maxPageNumber = maxItems / 10;
                if(maxItems % 10 != 0)
                {
                    maxPageNumber++;
                }
                return Ok(new {maxPageNumber});
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(Kullanici entity)
        {
            entity.isActive = true;
            entity.Password = ComputeSHA256Hash(entity.Password);
            var tmp = findAndReturn(entity);
           
            if(tmp == null || !tmp.isActive)
            {
                _context.Kullanici.Add(entity);
                Logger.log(_context,"Kullanici-Ekle",true);
                await _context.SaveChangesAsync();
               
                return CreatedAtAction(nameof(GetKullanici), new {id = entity.UserID}, entity);
            }
            else
            {
                Logger.log(_context,"Kullanici-Ekle-Hatali-Istek",false);
                await _context.SaveChangesAsync();
               
                return BadRequest();
            }

        }
        //sha 256 hashing
        private string ComputeSHA256Hash(string text)
        {
            using (var sha256 = new SHA256Managed())
            {
                return BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes(text))).Replace("-", "");
            }                
        }
        private Kullanici findAndReturn(Kullanici entity)
        {
            return _context.Kullanici.FirstOrDefault(x =>  x.Ad       == entity.Ad 
                                                        && x.Soyad    == entity.Soyad
                                                        && x.Email    == entity.Email
                                                        && x.Password == entity.Password
                                                        && x.Rol      == entity.Rol );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKullanici(int id, Kullanici entity)
        {
            
            if(id != entity.UserID)
            {
                Logger.log(_context,"Kullanici-Guncelle-Hatali-istek",false);
                await _context.SaveChangesAsync();
                return BadRequest();
            }
            

            var tmp = await _context.Kullanici.FindAsync(id);

            if(tmp == null)
            {
                Logger.log(_context,"Kullanici-Guncelle-Bulunamadi",false);
                await _context.SaveChangesAsync();
                return NotFound();
            }

            updateProperties(tmp, entity);
            Logger.log(_context,"Kullanici-Guncelle",true);
            await _context.SaveChangesAsync();
          
            return NoContent();
        }
        private void updateProperties(Kullanici tmp, Kullanici entity)
        {
            tmp.Ad       = entity.Ad;
            tmp.Soyad    = entity.Soyad;
            tmp.Email    = entity.Email;
            tmp.Password = ComputeSHA256Hash(entity.Password);
            tmp.Rol      = entity.Rol; 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKullanici(int id)
        {
            var tmp = await _context.Kullanici.FindAsync(id);
            
            if(tmp == null)
            {
                Logger.log(_context,"Kullanici-Sil-Bulunamadi",false);
                await _context.SaveChangesAsync();
                return NotFound();
            }

            tmp.isActive = false;
            
            Logger.log(_context,"Kullanici-Sil",true);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }
    }
}