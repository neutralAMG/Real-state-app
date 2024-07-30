

using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Core.Domain.Entities
{
    public class Perk
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }    
        public string Description { get; set; }
        public IList<PropertyPerk> PropertyPerks { get; set; }
    }
}
