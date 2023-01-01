using Flashcard.Data;
using Flashcard.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Flashcard.Controllers
{
    public class AccountController : Controller
    {
        AccountViewModel viewModel = new AccountViewModel();

        private readonly FlashcardContext _context;

        public AccountController(FlashcardContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // 認証済みであれば単語一覧画面へ遷移する
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(WordsController.Index), "Words");
            }
            return View(viewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(AccountViewModel viewModel, string returnUrl = null)
        {
            // もし入力エラーがあったら画面再表示して処理を中断する
            if (!ModelState.IsValid)
            {
                return View("Index", viewModel);
            }

            var users = from u in _context.Users
                        select u;

            // ユーザー名が一致するユーザーを抽出
            var lookupUser = users.Where(u => u.UserName == viewModel.UserName).FirstOrDefault();
            if (lookupUser == null)
            {
                viewModel.ErrorMsg = "ユーザ名かパスワードが間違っています。";
                return View("Index", viewModel);
            }

            // パスワードの比較
            if (lookupUser?.Password != viewModel.Password)
            {
                viewModel.ErrorMsg = "ユーザ名かパスワードが間違っています。";
                return View("Index", viewModel);
            }

            // Cookies 認証スキームで新しい ClaimsIdentity を作成し、ユーザー名を追加します。
            var identity = new ClaimsIdentity("MyCookieAuthenticationScheme");
            identity.AddClaim(new Claim(ClaimTypes.Name, lookupUser.UserName));

            // クッキー認証スキームと、上の数行で作成されたIDから作成された新しい ClaimsPrincipal を渡します。
            await HttpContext.SignInAsync("MyCookieAuthenticationScheme", new ClaimsPrincipal(identity));

            return RedirectToAction(nameof(WordsController.Index), "Words");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("MyCookieAuthenticationScheme");

            return RedirectToAction("Index", "Account");
        }
    }
}
