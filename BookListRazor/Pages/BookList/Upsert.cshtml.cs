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
    public class UpsertModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        [BindProperty]
        public Book Book { get; set; }

        public UpsertModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet(int? id)
        {
            Book = new Book();
            if (id == null)
            {
                return Page();
            }

            Book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (Book == null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) return Page();

            if (Book.Id == 0)
            {
                await _context.Books.AddAsync(Book);
            }
            else
            {
                _context.Books.Update(Book);
            }
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}