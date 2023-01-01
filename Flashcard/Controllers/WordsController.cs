using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Flashcard.Data;
using Flashcard.Models;
using Flashcard.ViewModels;
using System.Security.Claims;
using Microsoft.Extensions.Hosting;

namespace Flashcard.Controllers
{
    public class WordsController : Controller
    {
        private readonly FlashcardContext _context;

        private readonly Claim _claim;

        WordListViewModel viewModel = new WordListViewModel();

        public WordsController(FlashcardContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _claim = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        }

        // GET: Words
        public async Task<IActionResult> Index()
        {
            // ログインしていなければログイン画面へ
            if (_claim == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var flashcardContext = _context.Words
                .Where(w => w.UserId == userId);
            viewModel.WordList = await flashcardContext.ToListAsync();
            viewModel.UserId = userId;
            return View(viewModel);
        }

        // POST: Words/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WordId,Word,Meaning,UserId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt")] Words words)
        {
            // ログインしていなければログイン画面へ
            if (_claim == null)
            {
                return RedirectToAction("Index", "Account");
            }

            // もし入力エラーがあったら画面再表示して処理を中断する
            if (!ModelState.IsValid)
            {
                viewModel.ErrorMsg = "追加に失敗しました。";
                return RedirectToAction(nameof(Index));
            }
            
            // DBへ登録する
            _context.Add(words);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", words.UserId);
            return View(words);
        }

        // POST: Words/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WordId,Word,Meaning,UserId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt")] Words words)
        {
            if (id != words.WordId)
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
                    if (!WordsExists(words.WordId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", words.UserId);
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
                .Include(w => w.Users)
                .FirstOrDefaultAsync(m => m.WordId == id);
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
          return _context.Words.Any(e => e.WordId == id);
        }
    }
}
