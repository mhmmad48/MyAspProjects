using Microsoft.EntityFrameworkCore;

namespace BookStoreDev.Models
{
    public class BookstoreDevDbContext:DbContext
    {
        public BookstoreDevDbContext(DbContextOptions<BookstoreDevDbContext>options):base(options) 
        {
         
        }
        public DbSet<Author> Authors { get; set; }
        public  DbSet<Book> Books { get; set; }
    }
}
