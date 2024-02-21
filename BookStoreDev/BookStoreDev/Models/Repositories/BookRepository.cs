using Microsoft.EntityFrameworkCore;

namespace BookStoreDev.Models.Repositories
{
    public class BookRepository : IBookstoreDevRepository<Book>
    {
        BookstoreDevDbContext db;
        public BookRepository(BookstoreDevDbContext _db)
        {
            db= _db;
        }
        public async Task Add(Book entity)
        {
           db.Books.AddAsync(entity);
           db.SaveChanges();
        }

        public async Task Delete(int id)
        {
            var book= await db.Books.SingleOrDefaultAsync(x => x.Id == id);
             db.Books.Remove(book);
            db.SaveChanges();
        }

        public async Task<Book> Find(int id)
        {
            var book = await db.Books.Include(a=>a.Author).SingleOrDefaultAsync(x => x.Id == id);
            return book;
        }

        public  async Task Update(Book entity)
        {
            db.Update(entity);
            db.SaveChanges();
        }
        public  IList<Book> GetAll() {
          return  db.Books.Include(a=>a.Author).ToList();
        }
    }
}
