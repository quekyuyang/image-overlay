using Microsoft.AspNetCore.Mvc;
using ImageOverlay.Models;
using ImageOverlay.Data;

namespace ImageOverlay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnotatedImagesController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _context;

        public AnnotatedImagesController(IWebHostEnvironment webHostEnvironment, ApplicationDbContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        [HttpGet("{id}")]
        public ActionResult<Image> GetImage(int id)
        {
            var image = _context.Image.FirstOrDefault(entity => entity.Id == id);
            if (image == null) { return NotFound(); }
            image.FilePath = '/' + image.FilePath;
            return image;
        }

        [HttpGet("annotations/{id}")]
        public ActionResult<string> GetAnnotation(int id)
        {
            var annotationSet = _context.AnnotationSet.FirstOrDefault(entity => entity.Id == id);
            if (annotationSet == null || annotationSet.FilePath == null) { return NotFound(); }
            return annotationSet.FilePath;
        }

        [HttpPost]
        public string PostImage(IFormFile file)
        {
            string relPath = Path.Combine("images", Path.GetRandomFileName() + ".png");
            string filepath = Path.Combine(_webHostEnvironment.WebRootPath, relPath);
            using (var filestream = System.IO.File.Create(filepath))
            {
                file.CopyTo(filestream);
            }
            var image = new Image();
            image.Name = file.FileName;
            image.FilePath = relPath;
            _context.Image.Add(image);
            _context.SaveChanges();
            return filepath;
        }

        [HttpPost("{id}/annotations")]
        public ActionResult<string> AddAnnotationSet(IFormFile file, int id)
        {
            var image = _context.Image.FirstOrDefault(entity => entity.Id == id);
            if (image == null)
            {
                return BadRequest($"Image with Id {id} does not exist");
            }

            string relPath = Path.Combine("annotationSets", Path.GetRandomFileName() + ".json");
            var annotationSet = new AnnotationSet();
            annotationSet.FilePath = relPath;
            annotationSet.Image = image;
            annotationSet.DateTime = DateTime.Now;

            string filepath = Path.Combine(_webHostEnvironment.WebRootPath, relPath);
            using (var filestream = System.IO.File.Create(filepath))
            {
                file.CopyTo(filestream);
            }
            
            _context.AnnotationSet.Add(annotationSet);
            _context.SaveChanges();

            return filepath;
        }
    }
}
