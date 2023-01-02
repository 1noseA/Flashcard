using Microsoft.AspNetCore.Mvc;
using Flashcard.Data;
using Flashcard.Models;
using Flashcard.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Newtonsoft.Json;

namespace Flashcard.Controllers
{
    public class StudyController : Controller
    {
        private readonly FlashcardContext _context;

        private readonly Claim _claim;

        StudyViewModel viewModel = new StudyViewModel();
        Words words = new Words();

        public StudyController(FlashcardContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _claim = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        }

        // 初期処理
        // GET: Study
        public async Task<IActionResult> Index()
        {
            // ログインしていなければログイン画面へ
            if (_claim == null)
            {
                return RedirectToAction("Index", "Account");
            }

            // 問題番号・正解数を初期化する
            viewModel.QuestionNo = 1;
            viewModel.CorrectAnswerCount = 0;

            // ログインユーザに紐づく出題リストを作成する
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var flashcardContext = _context.Words
                .Where(w => w.UserId == userId)
                .OrderBy(x => Guid.NewGuid()) // シャッフルする
                .Take(10); // 10件取得
            viewModel.QuestionList = await flashcardContext.ToListAsync();

            // 出題リストを保存する
            TempData["QuestionList"] = JsonConvert.SerializeObject(viewModel.QuestionList);

            return View(viewModel);
        }

        // 答え合わせ処理
        // POST: Study/Answer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Answer(StudyViewModel viewModel)
        {
            // ログインしていなければログイン画面へ
            if (_claim == null)
            {
                return RedirectToAction("Index", "Account");
            }

            // 出題リスト取得
            viewModel.QuestionList = (List<Words>)TempData["QuestionList"];
            // 正答取得
            viewModel.CorrectAnswer = viewModel.QuestionList[0].Word;

            // 答え合わせする
            if (viewModel.MyAnswer == viewModel.CorrectAnswer)
            {
                viewModel.Judgement = "〇";
                // 正答数を加算する
                viewModel.CorrectAnswerCount++;
            }
            else
            {
                viewModel.Judgement = "×";
            }

            return View(viewModel);
        }

        // 次画面遷移処理
        // Get: Study/Next
        public IActionResult Next(StudyViewModel viewModel)
        {
            // ログインしていなければログイン画面へ
            if (_claim == null)
            {
                return RedirectToAction("Index", "Account");
            }

            // 初期化する
            viewModel.MyAnswer = "";
            viewModel.CorrectAnswer = "";
            viewModel.Judgement = "";

            // 出題リスト取得
            viewModel.QuestionList = (List<Words>)TempData["QuestionList"];
            // 出題済みの問題を出題リストから除く
            viewModel.QuestionList.Remove(viewModel.QuestionList[0]);

            // 出題リストがnullだったら結果画面に遷移する
            if (viewModel.QuestionList == null || viewModel.QuestionList.Count < 1)
            {
                return RedirectToAction("Index", "Result");
            }

            // 問題番号を加算する
            viewModel.QuestionNo++;
            return View(viewModel);
        }
    }
}
