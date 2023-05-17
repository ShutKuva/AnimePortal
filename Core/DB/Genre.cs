using System.Text.Json.Serialization;

namespace Core.DB
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        [JsonIgnore]
        public ICollection<AnimeDescription> AnimeDescriptions { get; set; } = new List<AnimeDescription>();
    }
}
