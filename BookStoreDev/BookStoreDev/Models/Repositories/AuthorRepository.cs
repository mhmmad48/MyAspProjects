using Microsoft.EntityFrameworkCore;

namespace BookStoreDev.Models.Repositories

{
    public class AuthorRepository : IBookstoreDevRepository<Author>
    {
        BookstoreDevDbContext db;
        public AuthorRepository(BookstoreDevDbContext _db)

        {
                db = _db;
        }
        public async Task Add(Author entity)
        {
            db.Authors.Add(entity);
            db.SaveChanges();
        }

        public async Task Delete(int id)
        {
            var author=db.Authors.SingleOrDefault(x => x.Id == id);
            db.Authors.Remove(author);
            db.SaveChanges();
        }

        public async Task<Author> Find(int id)
        {
            var author = db.Authors.SingleOrDefault(x => x.Id == id);
            return author;
        }

        public async Task Update(Author entity)
        {
            db.Update(entity);
            db.SaveChanges();
        }
        public  IList<Author> GetAll()
        {
            var authors =  db.Authors.ToList();
            return authors;
        }
    }
}
