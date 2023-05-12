using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
