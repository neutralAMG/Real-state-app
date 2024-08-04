

namespace FinalProject.Core.Application.Dtos.EntityDtos
{
    public class SellTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class SaveSellTypeDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UpdateSellTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
