using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Core.DB
{
    public class Language : BaseEntity
    {
        private string _name = string.Empty;

        public string Name
        {
            get => _name;
            set => _name = value.ToLower();
        }
    }
}
