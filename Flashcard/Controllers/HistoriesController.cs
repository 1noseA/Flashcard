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
        //public async Task<IActionResult> Index()
        public async Task<ChartJson> Index()
        {
            // ログインしていなければログイン画面へ
            //if (_claim == null)
            //{
            //    return RedirectToAction("Index", "Account");
            //}

            // 履歴を取得する
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var flashcardContext = _context.Histories
                .Where(h => h.UserId == userId)
                .OrderByDescending(h => h.StudyDate)
                .Take(10);
            var HistoriesList = await flashcardContext.ToListAsync();

            // グラフデータ作成
            string[] studyDate = new string[10];
            double?[] correctAnswerCount = new double?[10];
            double?[] correctAnswerRate = new double?[10];
            //foreach (var item in HistoriesList)
            for (int i = 0; i < HistoriesList.Count; i++)
            {
                studyDate[i] = HistoriesList[i].StudyDate.ToString("yyyy/MM/dd HH:mm");
                correctAnswerCount[i] = HistoriesList[i].CorrectAnswerCount;
                correctAnswerRate[i] = ((double)HistoriesList[i].CorrectAnswerCount / (double)HistoriesList[i].StudyCount * 100);
            }

            //// 画面表示項目を取得
            //viewModel.StudyDate = "'2022年12月30日'";
            //viewModel.CorrectAnswerCount = "1";

            return await Task.Run(() => {
                //var forecast = getForecast(startDate);
                return new ChartJson
                {
                    type = "bar",
                    // Dataにすると名前空間と被ってエラーになるため変更
                    data = new Datas
                    {
                        labels = studyDate,
                        datasets = new Dataset[] {
                                Dataset.CreateBar("正答数", correctAnswerCount, "royalblue"),
                            },
                    },
                    options = Options.Plain(),
                };
            });
            //return View(viewModel);
        }
    }
}
