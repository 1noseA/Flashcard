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
        Words words = new Words();

        public WordsController(FlashcardContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _claim = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        }

        // GET: Words
        public async Task<IActionResult> Index(WordListViewModel viewModel)
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
            words.UserId = userId;
            viewModel.Words = words;
            return View(viewModel);
        }

        // POST: Words/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WordId,Word,Meaning,UserId")] Words words)
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
                return RedirectToAction("Index", viewModel);
            }
            
            // DBへ登録する
            words.CreatedBy = User.Identity.Name;
            words.CreatedAt = DateTime.UtcNow;
            _context.Add(words);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Words/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WordId,Word,Meaning,UserId")] Words words)
        {
            // ログインしていなければログイン画面へ
            if (_claim == null)
            {
                return RedirectToAction("Index", "Account");
            }

            if (id != words.WordId)
            {
                viewModel.ErrorMsg = "更新に失敗しました。";
                return RedirectToAction("Index", viewModel);
            }

            // もし入力エラーがあったら画面再表示して処理を中断する
            if (!ModelState.IsValid)
            {
                viewModel.ErrorMsg = "更新に失敗しました。";
                return RedirectToAction("Index", viewModel);
            }
            else
            {
                try
                {
                    var updateData = _context.Words.Find(id);
                    updateData.Word = words.Word;
                    updateData.Meaning = words.Meaning;
                    //updateData.CreatedAt = updateData.CreatedAt.ToUniversalTime();
                    updateData.UpdatedBy = User.Identity.Name;
                    updateData.UpdatedAt = DateTime.UtcNow;
                    _context.Update(updateData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WordsExists(words.WordId))
                    {
                        viewModel.ErrorMsg = "更新に失敗しました。";
                        return RedirectToAction("Index", viewModel);
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Words/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            // ログインしていなければログイン画面へ
            if (_claim == null)
            {
                return RedirectToAction("Index", "Account");
            }

            if (_context.Words == null)
            {
                viewModel.ErrorMsg = "削除に失敗しました。";
                return RedirectToAction("Index", viewModel);
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
