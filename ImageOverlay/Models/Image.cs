namespace ImageOverlay.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string FilePath { get; set; } = "";

        public List<AnnotationSet> AnnotationSets { get; set; } = new List<AnnotationSet>();
    }
}
