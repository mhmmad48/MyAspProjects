using BookStoreDev.Models;
using BookStoreDev.Models.Repositories;
using BookStoreDev.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.Collections;
namespace BookStoreDev.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookstoreDevRepository<Book> bookRepository;
        private readonly IBookstoreDevRepository<Author> authorRepository;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hosting;

        public BookController(IBookstoreDevRepository<Book> bookRepository, IBookstoreDevRepository<Author> authorRepository, Microsoft.AspNetCore.Hosting.IHostingEnvironment hosting)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
            this.hosting = hosting;
        }
        // GET: BookController
        public async Task<ActionResult> Index()
        {
            var books =  bookRepository.GetAll().ToList();
            return View(books);
        }

        // GET: BookController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var book =await bookRepository.Find(id);
            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {

            return View(GetAllAuthors());
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BookAuthorViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var fileName =await UploadFile(model.NewFile) ?? string.Empty;

                    if (model.AuthorId == -1)
                    {
                        ViewBag.message = "please select author";
                        return View(GetAllAuthors());
                    }

                    var author =await authorRepository.Find(model.AuthorId);


                    var book = new Book
                    {
                        Id = model.BookId,
                        Title = model.Title,
                        Description = model.Description,
                        Author = author,
                        ImageUrl = fileName
                    };


                    await bookRepository.Add(book);
                    return RedirectToAction(nameof(Index));

                }
                catch
                {
                    return View();
                }
            }
            return View(model);
        }

        // GET: BookController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var book = await bookRepository.Find(id);
            var author = book.Author == null ? book.Author.Id = 0 : book.Author.Id;
            var authorlist = authorRepository.GetAll();
            var model = new BookAuthorViewModel
            {
                BookId = book.Id,
                Title = book.Title,
                Description = book.Description,
                AuthorId = author,
                AuthorName = authorlist.ToList(),
                
                ImageUrl = book.ImageUrl,

            };
            return View(model);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, BookAuthorViewModel model)
        {
            try
            {
                var filename = "";
             
                if(model.NewFile!=null) { 
                filename = model.NewFile.FileName;
                var rootpath = Path.Combine(hosting.WebRootPath, "uploads");
                var fullpath = Path.Combine(rootpath, filename);
                    var oldImgUrl = model.ImageUrl;
                    var fulloldpath = Path.Combine(rootpath, oldImgUrl);
                    System.IO.File.Delete(fulloldpath);
                 await model.NewFile.CopyToAsync(new FileStream(fullpath,FileMode.Create));
                }
                else { 
                filename =  model.ImageUrl;
                }
                var author = await authorRepository.Find(model.AuthorId);
                var book = new Book
                {
                    Id = model.BookId,
                    Title = model.Title,
                    Description = model.Description,
                    Author = author,
                    ImageUrl = filename
                };
                await bookRepository.Update(book);
                return RedirectToAction(nameof(Index));
            }

            catch
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var book = await bookRepository.Find(id);
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
               await bookRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public List<Author> FillSelectList()
        {
            var authors = authorRepository.GetAll().ToList();
             authors.Insert(0, new Author { Id = -1, FullName = "Plese Select Author" });
            return authors;
        }
        public BookAuthorViewModel GetAllAuthors()
        {
            var authors = FillSelectList();
            var model = new BookAuthorViewModel
            {
                AuthorName = authors
            };
            return model;
        }
        public async Task<string> UploadFile(IFormFile file)
        {
            var rootpath = Path.Combine(hosting.WebRootPath, "uploads");
            var fullpath = Path.Combine(rootpath, file.FileName);
            await file.CopyToAsync(new FileStream(fullpath, FileMode.Create));
            return file.FileName;
        }

    }
}
