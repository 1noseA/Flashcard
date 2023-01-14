using Microsoft.AspNetCore.Mvc;
using Flashcard.Data;
using System.Security.Claims;
using Flashcard.ViewModels;
using Newtonsoft.Json;
using Flashcard.Models;
using NuGet.Packaging.Signing;

namespace Flashcard.Controllers
{
    public class ResultController : Controller
    {
        private readonly FlashcardContext _context;

        private readonly Claim _claim;

        StudyViewModel viewModel = new StudyViewModel();

        public ResultController(FlashcardContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _claim = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        }

        // 初期処理
        // GET: Result
        public async Task<IActionResult> Index()
        {
            // ログインしていなければログイン画面へ
            if (_claim == null)
            {
                return RedirectToAction("Index", "Account");
            }

            // 画面表示項目を取得
            viewModel.ResultList = JsonConvert.DeserializeObject<List<ResultViewModel>>((string)TempData["ResultList"]);
            viewModel.CorrectAnswerCount = (int)TempData["CorrectAnswerCount"];

            // 同日の履歴があるか確認する
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var today = DateTime.Now.ToString("yyyy-MM-dd");
            Histories history = _context.Histories
                .FirstOrDefault(h => h.UserId == userId && h.StudyDate.ToString() == today);
            // 同日の履歴があれば更新
            if (history != null)
            {
                history.StudyCount = history.StudyCount + viewModel.ResultList.Count;
                history.CorrectAnswerCount = viewModel.CorrectAnswerCount;
                history.UpdatedBy = User.Identity.Name;
                history.UpdatedAt = DateTime.Now;
                _context.Update(history);
                await _context.SaveChangesAsync();
            }
            // なければ登録
            else
            {
                Histories histories = new Histories();
                histories.UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                histories.StudyDate = DateOnly.Parse(today);
                histories.StudyCount = viewModel.ResultList.Count;
                histories.CorrectAnswerCount = viewModel.CorrectAnswerCount;
                histories.CreatedBy = User.Identity.Name;
                histories.CreatedAt = DateTime.Now;
                _context.Add(histories);
                await _context.SaveChangesAsync();
            }
            return View(viewModel);
        }
    }
}
