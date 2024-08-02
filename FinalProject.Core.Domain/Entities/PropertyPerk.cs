

using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Core.Domain.Entities
{
    public class PropertyPerk
    {
        public PropertyPerk()
        {
            DateCreated = DateTime.UtcNow; 
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        public int PerkId { get; set; }
        public Guid PropertyId { get; set; }
        public Perk Perk { get; set; }
        public Property Property { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
