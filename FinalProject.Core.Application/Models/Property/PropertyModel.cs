﻿
using FinalProject.Core.Application.Models.Perk;
using FinalProject.Core.Application.Models.PropertyImgae;

namespace FinalProject.Core.Application.Models.Property
{
    public class PropertyModel
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
        public string SellTypeName { get; set; }
        //public PropertyType PropertyType { get; set; }
        public string PropertyTypeName { get; set; }

        //public IList<PropertyImage>? PropertyImages { get; set; }
        public  List<PropertyImageModel> PropertyImagesUrls { get; set; }
        //public IList<FavoriteUserProperty>? FavoriteUsersProperties { get; set; }
        public bool IsMarkAsFavoriteByCurrentUser  { get; set; }
        public List<PerkModel> PropertyPerks { get; set; }
    }
}
