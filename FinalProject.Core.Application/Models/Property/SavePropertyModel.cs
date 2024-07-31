

using FinalProject.Core.Application.Models.Perk;
using FinalProject.Core.Application.Models.PropertyImgae;
using Microsoft.AspNetCore.Http;

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
        public List<string> PropertyImagesUrls { get; set; }
        // Ditionary to update each coresponding file, file may be null;
        public Dictionary<string, IFormFile> ImagesToUpdateAndItsFiles {  get; set; }
        public List<IFormFile> PropertyImagesFiles { get; set; }
        public List<int> PropertyPerks { get; set; }
    }
}
