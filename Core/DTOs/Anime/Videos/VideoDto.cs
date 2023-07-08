namespace Core.DTOs.Anime.Videos
{
    public class VideoDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public ICollection<PlayerDto> Players { get; set; }
    }
}