using Microsoft.AspNetCore.Mvc;
using Flashcard.Data;
using System.Security.Claims;
using Flashcard.ViewModels;
using Newtonsoft.Json;
using Flashcard.Models;

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
        public IActionResult Index()
        {
            // ログインしていなければログイン画面へ
            if (_claim == null)
            {
                return RedirectToAction("Index", "Account");
            }

            viewModel.ResultList = JsonConvert.DeserializeObject<List<ResultViewModel>>((string)TempData["ResultList"]);
            viewModel.CorrectAnswerCount = (int)TempData["CorrectAnswerCount"];

            return View(viewModel);
        }
    }
}
