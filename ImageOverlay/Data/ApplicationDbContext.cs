using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ImageOverlay.Models;

namespace ImageOverlay.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Define your entity DbSet properties here
        public DbSet<Image> Image { get; set; }

        public DbSet<AnnotationSet> AnnotationSet { get; set; }
    }
}
