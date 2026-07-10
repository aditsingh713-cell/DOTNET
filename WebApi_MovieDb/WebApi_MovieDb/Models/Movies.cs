using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi_MovieDb.Models
{
    public class Movies
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string? Director { get; set; }
        public int GenreId { get; set; }
        [ForeignKey("GenreId")]
        public virtual Genres GenreNavigation { get; set; } = null!;
    }
}
