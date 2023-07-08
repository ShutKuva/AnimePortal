using Core.BaseEntities;

namespace Core.DB.Videos
{
    public class CloudinaryVideo : CloudinaryEntity
    {
        public int VideoId { get; set; }
        public Video Video { get; set; }
    }
}