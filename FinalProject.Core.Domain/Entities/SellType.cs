
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Core.Domain.Entities
{
    public class SellType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Property> Properties { get; set; }
    }
}
