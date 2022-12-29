using FavoritesAPI.Data;
using FavoritesAPI.Data.Models;
using FavoritesAPI.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FavoritesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly DataContext _context;
        private IConfiguration _configuration;
        private IHttpContextAccessor _acc;

        public FavoritesController(DataContext context, IConfiguration configuration, IHttpContextAccessor acc)
        {
            _configuration = configuration;
            _context = context;
            _acc = acc;
        }

        [HttpPost("add-favorite"), Authorize]

        public async Task<IActionResult> AddToFavorite([FromBody] FavoritesVM favorite)
        {

            var userId = int.Parse(_acc.HttpContext.User.FindFirstValue(ClaimTypes.PrimarySid));
            var favSadrzaj = new Favorites();
            if (!_context.Favorites.Any(f => f.IdSadrzaja == favorite.IdSadrzaja && f.Tip == favorite.Tip))
            {
                favSadrzaj = new Favorites
                {
                    IdSadrzaja = favorite.IdSadrzaja,
                    Tip = favorite.Tip
                };
                _context.Favorites.Add(favSadrzaj);
                await _context.SaveChangesAsync();
            }
            else
            {
                favSadrzaj = await _context.Favorites.Where(f => f.IdSadrzaja == favorite.IdSadrzaja && f.Tip == favorite.Tip).FirstOrDefaultAsync();
            }

            var favUser = new User_Favorites
            {
                UserId = userId,
                FavoritesId = favSadrzaj.Id,
            };
            _context.User_Favorites.Add(favUser);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Uspesno dodano" });

        }

        [HttpPost("test")]

        public async Task<IActionResult> PostMessage()
        {
            try
            {
                var userId = int.Parse(_acc.HttpContext.User.FindFirstValue(ClaimTypes.PrimarySid));
                var token = HttpContext.Request.Headers["Authorization"].ToString();
                return Ok(new { message = token });
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("get-favorites"), Authorize]

        public async Task<IActionResult> GetFavorites()
        {
            var userId = int.Parse(_acc.HttpContext.User.FindFirstValue(ClaimTypes.PrimarySid));
            var sadrzaj = await _context.User_Favorites.Where(e => e.UserId == userId).Include(e => e.Favorites).ToListAsync();
            return Ok(sadrzaj);
        }


        [HttpDelete("delete-favorite/{tip}/{id}"), Authorize]

        public async Task<IActionResult> DeleteFavorites(string tip, int id)
        {
            var userId = int.Parse(_acc.HttpContext.User.FindFirstValue(ClaimTypes.PrimarySid));
            var favSadrzaj = await _context.Favorites.Where(f => f.IdSadrzaja == id && f.Tip == tip).FirstOrDefaultAsync();
            var rel = await _context.User_Favorites.Where(f => f.UserId == userId && f.FavoritesId == favSadrzaj.Id).FirstOrDefaultAsync();
            _context.User_Favorites.Remove(rel);
            _context.SaveChanges();


            return Ok(new { message = "Relation removed" });
        }
    }
}
