namespace FinalProject.Presentation.WebApp.Models
{
    public class CheckBoxViewModel
    {
        public CheckBoxViewModel()
        {
            IsSelected = false;
        }
        public string Name { get; set; }
        public int Value { get; set; }
        public bool IsSelected { get; set; }
    }
}
