using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Flashcard.Data;
using Flashcard.Models;

namespace Flashcard.Controllers
{
    public class WordsController : Controller
    {
        private readonly FlashcardContext _context;

        public WordsController(FlashcardContext context)
        {
            _context = context;
        }

        // GET: Words
        public async Task<IActionResult> Index()
        {
              return View(await _context.Words.ToListAsync());
        }

        // GET: Words/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Words == null)
            {
                return NotFound();
            }

            var words = await _context.Words
                .FirstOrDefaultAsync(m => m.Id == id);
            if (words == null)
            {
                return NotFound();
            }

            return View(words);
        }

        // GET: Words/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Words/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Word,Meaning,UserId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt")] Words words)
        {
            if (ModelState.IsValid)
            {
                _context.Add(words);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(words);
        }

        // GET: Words/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Words == null)
            {
                return NotFound();
            }

            var words = await _context.Words.FindAsync(id);
            if (words == null)
            {
                return NotFound();
            }
            return View(words);
        }

        // POST: Words/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Word,Meaning,UserId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt")] Words words)
        {
            if (id != words.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(words);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WordsExists(words.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(words);
        }

        // GET: Words/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Words == null)
            {
                return NotFound();
            }

            var words = await _context.Words
                .FirstOrDefaultAsync(m => m.Id == id);
            if (words == null)
            {
                return NotFound();
            }

            return View(words);
        }

        // POST: Words/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Words == null)
            {
                return Problem("Entity set 'FlashcardContext.Words'  is null.");
            }
            var words = await _context.Words.FindAsync(id);
            if (words != null)
            {
                _context.Words.Remove(words);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WordsExists(int id)
        {
          return _context.Words.Any(e => e.Id == id);
        }
    }
}
