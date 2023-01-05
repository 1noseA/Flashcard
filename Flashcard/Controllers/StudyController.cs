using Microsoft.AspNetCore.Mvc;
using Flashcard.Data;
using Flashcard.Models;
using Flashcard.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Newtonsoft.Json;
using System.Collections.Generic;

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

            // 初期化する
            TempData.Remove("ResultList");
            viewModel.QuestionNo = 1;
            viewModel.CorrectAnswerCount = 0;
            // 保存する
            TempData["QuestionNo"] = viewModel.QuestionNo;
            TempData["CorrectAnswerCount"] = viewModel.CorrectAnswerCount;

            // ログインユーザに紐づく出題リストを作成する
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var flashcardContext = _context.Words
                .Where(w => w.UserId == userId)
                .OrderBy(x => Guid.NewGuid()) // シャッフルする
                .Take(2); // 10件取得 【TODO】10に戻す
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

            // 保存データを取得
            viewModel.QuestionNo = (int)TempData["QuestionNo"];
            viewModel.CorrectAnswerCount = (int)TempData["CorrectAnswerCount"];
            viewModel.QuestionList = JsonConvert.DeserializeObject<List<Words>>((string)TempData["QuestionList"]);
            TempData.Keep();

            // 正答取得
            viewModel.CorrectAnswer = viewModel.QuestionList[0].Word;
            // 答え合わせする
            if (viewModel.MyAnswer == viewModel.CorrectAnswer)
            {
                viewModel.Judgement = "〇";
                // 正答数を加算する
                viewModel.CorrectAnswerCount++;
                TempData["CorrectAnswerCount"] = viewModel.CorrectAnswerCount;
            }
            else
            {
                viewModel.Judgement = "×";
            }
            // 回答を再設定する
            viewModel.MyAnswer = viewModel.MyAnswer;
            // チェック済みにする
            viewModel.ChekedFlg = true;

            // 結果リストに格納する
            List<ResultViewModel> resultList = new List<ResultViewModel>();
            ResultViewModel result = new ResultViewModel();
            result.QuestionNo = viewModel.QuestionNo;
            result.Meaning = viewModel.QuestionList[0].Meaning;
            result.MyAnswer = viewModel.MyAnswer;
            result.CorrectAnswer = viewModel.CorrectAnswer;
            result.Judgement = viewModel.Judgement;
            // すでに結果リストがあったら追加する
            if (TempData["ResultList"] != null)
            {
                resultList = JsonConvert.DeserializeObject<List<ResultViewModel>>((string)TempData["ResultList"]);
                resultList.Add(result);
                TempData["ResultList"] = JsonConvert.SerializeObject(resultList);
            }
            else
            {
                resultList.Add(result);
                TempData["ResultList"] = JsonConvert.SerializeObject(resultList);
            }

            return View("Index", viewModel);
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

            // 保存データを取得
            viewModel.QuestionNo = (int)TempData["QuestionNo"];
            viewModel.CorrectAnswerCount = (int)TempData["CorrectAnswerCount"];
            viewModel.QuestionList = JsonConvert.DeserializeObject<List<Words>>((string)TempData["QuestionList"]);
            TempData.Keep();

            // 出題済みの問題を出題リストから除く
            viewModel.QuestionList.Remove(viewModel.QuestionList[0]);

            // 出題リストが0だったら結果画面に遷移する
            if (viewModel.QuestionList.Count < 1)
            {
                return RedirectToAction("Index", "Result");
            }

            // 出題リストを保存する
            TempData["QuestionList"] = JsonConvert.SerializeObject(viewModel.QuestionList);

            // 問題番号を加算する
            viewModel.QuestionNo++;
            TempData["QuestionNo"] = viewModel.QuestionNo;

            return View("Index", viewModel);
        }
    }
}
