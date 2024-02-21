using BookStoreDev.Models;
using System.ComponentModel.DataAnnotations;

namespace BookStoreDev.ViewModels
{
    public class BookAuthorViewModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        [StringLength(30,MinimumLength =6)]
        public string Description { get; set; }


        [Required]
        public int AuthorId { get; set; }
      
        public List<Author>? AuthorName { get; set; }
        public IFormFile NewFile { get; set; }
        public string? ImageUrl { get; set; }
    }
}
