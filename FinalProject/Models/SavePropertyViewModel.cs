using FinalProject.Core.Application.Models.Property;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProject.Presentation.WebApp.Models
{
	public class SavePropertyViewModel
	{
		public SavePropertyModel SavePropertyModel { get; set; }
		public List<SelectListItem> PropertyTypes { get; set; }
		public List<SelectListItem> SellTypes { get; set; }
		public List<CheckBoxViewModel> perks { get; set; }
	}

    public class SavePropertyViewModelWithData
    {
        public PropertyModel SavePropertyModel { get; set; }
        public List<SelectListItem> PropertyTypes { get; set; }
        public List<SelectListItem> SellTypes { get; set; }
        public List<CheckBoxViewModel> perks { get; set; }
    }
}
