using BookStoreDev.Models;
using BookStoreDev.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreDev.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IBookstoreDevRepository<Author> authorRepository;

        public AuthorController(IBookstoreDevRepository<Author>AuthorRepository)
        {
            authorRepository = AuthorRepository;
        }
        // GET: AuhtorController
        public ActionResult Index()
        {
            var author = authorRepository.GetAll();
            return View(author);
        }

        // GET: AuhtorController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var author = await authorRepository.Find(id);
            return View(author);
        }

        // GET: AuhtorController/Create
        public ActionResult Create()
        {
           
            return View();
        }

        // POST: AuhtorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Author author)
        {
            try
            {
                await authorRepository.Add(author);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuhtorController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var author = await authorRepository.Find(id);
            return View(author);
        }

        // POST: AuhtorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Author author)
        {
            try
            {
                await authorRepository.Update(author);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuhtorController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var author =await authorRepository.Find(id);
            return View(author);
        }

        // POST: AuhtorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Author author)
        {
            try
            {
                await authorRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
