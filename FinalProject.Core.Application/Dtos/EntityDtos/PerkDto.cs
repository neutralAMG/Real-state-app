

namespace FinalProject.Core.Application.Dtos.EntityDtos
{
    public class PerkDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class SavePerkDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UpdatePerkDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
