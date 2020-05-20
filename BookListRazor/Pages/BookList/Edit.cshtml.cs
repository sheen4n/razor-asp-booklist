using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        [BindProperty]
        public Book Book { get; set; }

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGet(int id)
        {
            Book = await _context.Books.FindAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) return Page();

            var bookFromDb = await _context.Books.FindAsync(Book.Id);
            bookFromDb.Name = Book.Name;
            bookFromDb.Author = Book.Author;
            bookFromDb.ISBN = Book.ISBN;

            await _context.SaveChangesAsync();
            return RedirectToPage("Index");

        }
    }
}