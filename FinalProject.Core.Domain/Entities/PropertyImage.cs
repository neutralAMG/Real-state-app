

namespace FinalProject.Core.Domain.Entities
{
    public class PropertyImage
    {
        public PropertyImage()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }    
        public string? ImgUrl { get; set; }
        public Guid PropertyId { get; set; }
        public Property Property { get; set; }
    }
}
