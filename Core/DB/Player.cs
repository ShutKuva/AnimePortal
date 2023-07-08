using Core.DB.Videos;

namespace Core.DB
{
    public class Player : BaseEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }
        
        public int VideoId { get; set; }
        public Video Video { get; set; }
    }
}