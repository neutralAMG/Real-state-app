

using FinalProject.Core.Application.Models.Perk;
using FinalProject.Core.Application.Models.PropertyImgae;

namespace FinalProject.Core.Application.Models.Property
{
    public class SavePropertyModel
    {
        public Guid Id { get; set; }
        public string PropertyCode { get; set; }
        public string Description { get; set; }

        public decimal PropertyPrice { get; set; }

        public int AmountOfBathrooms { get; set; }
        public int AmountOfBedrooms { get; set; }
        public decimal SizeInMeters { get; set; }
        public string AgentId { get; set; }
        //public SellType SellType { get; set; }
        public int SellTypeId { get; set; }
        //public PropertyType PropertyType { get; set; }
        public int PropertyTypeId { get; set; }
        List<SavePropertyImageModel> PropertyImagesUrls { get; set; }

        public List<SavePerkModel> PropertyPerks { get; set; }
    }
}
