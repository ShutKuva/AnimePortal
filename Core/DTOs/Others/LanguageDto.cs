
namespace Core.DTOs.Others
{
    public class LanguageDto
    {
        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set => _name = value.ToLower();
        }
    }
}
