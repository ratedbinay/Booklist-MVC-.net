﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookListMVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BooksController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Books.ToList());
        }

        //GET Book/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book Book)
        {
            if (ModelState.IsValid)
            {
                _db.Add(Book);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            return View(Book);
        }

        //GET edit book
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _db.Books.SingleOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);

        }

        //Edit POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                //_db.Update(book);
                var BookFromDb = await _db.Books.FirstOrDefaultAsync(b => b.Id == book.Id);
                BookFromDb.Name = book.Name;
                BookFromDb.Auther = book.Auther;
                BookFromDb.Price = book.Price;

                await _db.SaveChangesAsync();
                return RedirectToPage(nameof(Index));

            }
            return View(book);
        }


        //GET Delete book
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _db.Books.SingleOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);

        }

        //Delete POST

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveBook(int? id)
        {
            var book = await _db.Books.SingleOrDefaultAsync(m => m.Id == id);
            _db.Books.Remove(book);

            await _db.SaveChangesAsync();
            return RedirectToPage(nameof(Index));


        }


    }
}