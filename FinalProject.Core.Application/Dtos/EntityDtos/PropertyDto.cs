

using FinalProject.Core.Domain.Entities;

namespace FinalProject.Core.Application.Dtos.EntityDtos
{
    public class PropertyDto
    {
        public Guid Id { get; set; }
        public string PropertyCode { get; set; }
        public string PropertyType { get; set; }
        public string SellType { get; set; }
        public decimal PropertyPrice { get; set; }
        public decimal SizeInMeters { get; set; }
        public int AmountOfBedrooms { get; set; }
        public int AmountOfBathrooms { get; set; }
        public string Description { get; set; }
        List<Perk> perks { get; set; }
        public string AgentId { get; set; }
        public string AgentName { get; set; }

    }

}
