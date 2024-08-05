

using Microsoft.AspNetCore.Http;

namespace FinalProject.Core.Application.Models.PropertyImgae
{
    public class SavePropertyImageModel
    {
        public Guid Id { get; set; }
        public string ImgUrl { get; set; }
        public Guid PropertyId { get; set; }
        public IFormFile file { get; set; }


	}
}
