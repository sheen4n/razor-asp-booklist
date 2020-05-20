using System.Collections.Generic;
using System.Threading.Tasks;
using BookListRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
   
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IEnumerable<Book> Books { get; set; }
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task OnGet()
        {
            Books = await _context.Books.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null) return NotFound();

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}

