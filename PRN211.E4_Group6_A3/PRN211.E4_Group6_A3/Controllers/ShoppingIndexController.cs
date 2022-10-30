using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRN211.E4_Group6_A3.Models;

namespace PRN211.E4_Group6_A3.Controllers
{
    public class ShoppingIndexController : Controller
    {
        private readonly MusicStoreContext _context;
        private readonly IWebHostEnvironment _environment;

        public ShoppingIndexController(MusicStoreContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public async Task<IActionResult> Index(int genreId, string currentTitle, [FromQuery]int page = 0)
        {
            if (currentTitle == null)
            {
                currentTitle = "";
            }
            List<SelectListItem> genres = new SelectList(_context.Genres, "GenreId", "Name", genreId).ToList();
            genres.Insert(0, new SelectListItem { Value = "0", Text = "All" });
            ViewData["GenreIds"] = genres;

            var totalRecord = _context.Albums.Include(a => a.Artist).Include(a => a.Genre)
                .Where(a => a.GenreId == (genreId == 0 ? a.GenreId : genreId)
                && a.Title.Contains(currentTitle ?? "")).Count();

            int totalPage = totalRecord / 3;
            if (totalRecord % 3 != 0)
            {
                totalPage++;
            }

            var musicStoreContext = _context.Albums.Include(a => a.Artist).Include(a => a.Genre)
                .Where(a => a.GenreId == (genreId == 0 ? a.GenreId : genreId)
                && a.Title.Contains(currentTitle ?? "")).Skip(page * 3).Take(3);
            ViewData["CurrentTitle"] = currentTitle;
            ViewData["TotalPage"] = totalPage;
            ViewData["CurrentPage"] = page;
            ViewData["GenreId"] = genreId;
            return View(await musicStoreContext.ToListAsync());
        }
    }
}
