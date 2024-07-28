

namespace FinalProject.Core.Application.Models.FavoriteUserProperty
{
    internal class SaveFavoriteUserPropertyModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public Guid PropertyId { get; set; }
    }
}
