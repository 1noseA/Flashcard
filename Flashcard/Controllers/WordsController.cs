using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Flashcard.Data;
using Flashcard.Models;
using System.Security.Claims;

namespace Flashcard.Controllers
{
    public class WordsController : Controller
    {
        private readonly FlashcardContext _context;

        private readonly Claim _claim;

        public WordsController(FlashcardContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _claim = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        }

        // GET: Words
        public async Task<IActionResult> Index(int? pageNumber)
        {
            // ログインしていなければログイン画面へ
            if (_claim == null)
            {
                return RedirectToAction("Index", "Account");
            }
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var wordList = _context.Words
                                .Where(w => w.UserId == userId);

            // 登録・更新・削除失敗時以外はエラーメッセージを初期化する
            if (TempData["RedirectFlg"] == null || (bool)TempData["RedirectFlg"] != true) {
                TempData["ErrorMsg"] = "";
            }
            
            TempData["UserId"] = userId;

            // ページング
            int pageSize = 20;
            return View(await PaginatedList<Words>.CreateAsync(wordList, pageNumber ?? 1, pageSize));
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

            // 入力エラーがあったら画面再表示して処理を中断する
            if (!ModelState.IsValid)
            {
                TempData["ErrorMsg"] = "追加に失敗しました。";
                TempData["RedirectFlg"] = true;
                return RedirectToAction("Index");
            }

            // DBへ登録する
            words.CreatedBy = User.Identity.Name;
            words.CreatedAt = DateTime.Now;
            _context.Add(words);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Words/Edit/5
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
                TempData["ErrorMsg"] = "更新に失敗しました。";
                TempData["RedirectFlg"] = true;
                return RedirectToAction("Index");
            }

            // 入力エラーがあったら画面再表示して処理を中断する
            if (!ModelState.IsValid)
            {
                TempData["ErrorMsg"] = "更新に失敗しました。";
                TempData["RedirectFlg"] = true;
                return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    var updateData = _context.Words.Find(id);
                    updateData.Word = words.Word;
                    updateData.Meaning = words.Meaning;
                    updateData.UpdatedBy = User.Identity.Name;
                    updateData.UpdatedAt = DateTime.Now;
                    _context.Update(updateData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WordsExists(words.WordId))
                    {
                        TempData["ErrorMsg"] = "更新に失敗しました。";
                        TempData["RedirectFlg"] = true;
                        return RedirectToAction("Index");
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
                TempData["ErrorMsg"] = "削除に失敗しました。";
                TempData["RedirectFlg"] = true;
                return RedirectToAction("Index");
            }
            var words = await _context.Words.FindAsync(id);
            if (words != null)
            {
                _context.Words.Remove(words);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool WordsExists(int id)
        {
          return _context.Words.Any(e => e.WordId == id);
        }
    }
}
