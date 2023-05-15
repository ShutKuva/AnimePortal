using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.DB
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        [NotMapped]
        public ICollection<AnimeDescription> AnimeDescriptions { get; set; }
    }
}
