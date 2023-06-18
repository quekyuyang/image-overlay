namespace ImageOverlay.Models
{
    public class AnnotationSet
    {
        public int Id { get; set; }

        public string? FilePath { get; set; }

        public Image? Image { get; set; }

        public DateTime DateTime { get; set; }
    }
}
