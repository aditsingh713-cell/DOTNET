namespace WebApi_MovieDb.Models
{
    public class MovieInputModel
    {
        public string Title { get; set; } = null!;
        public int ReleaseYear { get; set; }
        public string? Director { get; set; }
        public int GenreId { get; set; }
    }
}
