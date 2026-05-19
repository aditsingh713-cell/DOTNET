using System.ComponentModel.DataAnnotations;

namespace BookLibraryApp.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Author { get; set; }

        [Display(Name = "Published Date")]
        public DateTime PublishedDate { get; set; }

        public string Genre { get; set; }
    }
}