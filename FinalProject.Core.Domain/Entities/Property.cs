

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Core.Domain.Entities
{
    public class Property
    {
        public Property()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string PropertyCode { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal PropertyPrice { get; set; }

        public int AmountOfBathrooms { get; set; }
        public int AmountOfBedrooms { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal SizeInMeters { get; set; }

        public int SellTypeId { get; set; }
        public int PropertyTypeId { get; set; }
        public string AgentId { get; set; }

        public SellType SellType { get; set; }
        public PropertyType PropertyType { get; set; }

        public IList<PropertyImage>? PropertyImages { get; set; }
        public IList<FavoriteUserProperty>? FavoriteUsersProperties { get; set; }
        public IList<PropertyPerk> PropertyPerks { get; set; }

    }
}
