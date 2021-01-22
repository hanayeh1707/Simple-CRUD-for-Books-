using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookProject.Controllers
{
    public class BookController : Controller
    {
        private readonly BookManager bookManager; 
        public BookController()
        {
            bookManager = new BookManager(); 
        }
        // GET: BookController
        public ActionResult Index(string searchKeyword="")
        {
            if (string.IsNullOrEmpty(searchKeyword))
                return View(bookManager.ListBooks());
            else
                return View(bookManager.Find(searchKeyword));
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            return View(bookManager.GetBookDetails(id));
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookViewModel bookViewModel)
        {
            try
            {
                bookManager.AddBook(bookViewModel);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(bookManager.GetBookDetails(id));
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookViewModel bookViewModel)
        {
            try
            {
                bookManager.UpdateBook(bookViewModel);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(bookManager.GetBookDetails(id));
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                bookManager.RemoveBook(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult SearchBook(string Keyword)
        {
            return View(bookManager.Find(Keyword));
        }
    }
}
