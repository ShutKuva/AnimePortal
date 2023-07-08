using Core.BaseEntities;
using Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Core.DB.Videos
{
    public class Video : BaseEntity
    {
        public string Title { get; set; }
        public VideoTypes VideoType { get; set; }
        public ICollection<Player> Players { get; set; }

        public int LocalizationId { get; set; }
        public Localization Localization { get; set; }
    }
}