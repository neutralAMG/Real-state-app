
namespace FinalProject.Core.Application.Dtos.EntityDtos
{
    public class PropertyTypeDto 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class SavePropertyTypeDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
