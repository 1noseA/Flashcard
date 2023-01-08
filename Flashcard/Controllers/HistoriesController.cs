using Microsoft.AspNetCore.Mvc;
using Flashcard.Data;
using System.Security.Claims;
using Flashcard.ViewModels;
using Microsoft.EntityFrameworkCore;
using Flashcard.Chart;

namespace Flashcard.Controllers
{
    public class HistoriesController : Controller
    {
        private readonly FlashcardContext _context;

        private readonly Claim _claim;

        HistoriesViewModel viewModel = new HistoriesViewModel();

        public HistoriesController(FlashcardContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _claim = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        }

        // 初期処理
        // GET: Histories
        public async Task<IActionResult> Index()
        {
            // ログインしていなければログイン画面へ
            if (_claim == null)
            {
                return RedirectToAction("Index", "Account");
            }

            // 履歴を取得する
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var flashcardContext = _context.Histories
                .Where(h => h.UserId == userId)
                .OrderByDescending(h => h.StudyDate)
                .Take(10);
            var HistoriesList = await flashcardContext.ToListAsync();

            // グラフデータ作成
            foreach (var item in HistoriesList)
            {
                if (!string.IsNullOrEmpty(viewModel.StudyDate))
                {
                    viewModel.StudyDate = viewModel.StudyDate + ", '" + item.StudyDate.ToString("yyyy/MM/dd HH:mm") + "'";
                    viewModel.CorrectAnswerCount = viewModel.CorrectAnswerCount + ", " + item.CorrectAnswerCount.ToString();
                    viewModel.CorrectAnswerRate = viewModel.CorrectAnswerRate + ", " + ((double)item.CorrectAnswerCount / (double)item.StudyCount * 100).ToString();
                }
                else
                {
                    viewModel.StudyDate = "'" + item.StudyDate.ToString("yyyy/MM/dd HH:mm") + "'";
                    viewModel.CorrectAnswerCount = item.CorrectAnswerCount.ToString();
                    viewModel.CorrectAnswerRate = ((double)item.CorrectAnswerCount / (double)item.StudyCount * 100).ToString();
                }
            }

            return View(viewModel);
        }
    }
}
