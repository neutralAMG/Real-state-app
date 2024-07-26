

namespace FinalProject.Core.Application.Models.Property
{
    public class PropertyFilterModel
    {
        public int MinBathrooms { get; set; }
        public int MaxBathrooms { get; set; }
        public int MinBedrooms { get; set; }
        public int MaxBedrooms { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public int PropertyType { get; set; }
        public int MinSize { get; set; }
        public int MaxSize { get; set; }
    }
}
