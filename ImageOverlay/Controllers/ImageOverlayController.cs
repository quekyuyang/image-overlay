using ImageOverlay.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ImageOverlay.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageOverlay.Controllers
{
    public class ImageOverlayController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ImageOverlayController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult ImageOverlay(int id)
        {
            var img = _context.Image
                .Include(entity => entity.AnnotationSets)
                .FirstOrDefault(entity => entity.Id == id);
            img.FilePath = '/' + img.FilePath;
            //Url.Content()
            return View(img);
        }

        public IActionResult Search(string? searchString)
        {
            if (!searchString.IsNullOrEmpty())
            {
                var images = _context.Image.Where(entity => entity.Name.Contains(searchString!)).ToList();
                return View(images);
            }

            return View(new List<Image>());
        }
    }
}
