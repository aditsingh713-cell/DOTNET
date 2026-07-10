namespace WebApi_MovieDb.Models
{
    public class Genres
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<Movies> Movies { get; set; } = new List<Movies>();
    }
}
