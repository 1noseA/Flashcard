using Flashcard.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Flashcard.Controllers
{
    public class AccountController : Controller
    {
        // 仮
        List<Users> users = new List<Users> {
        new Users{Id = 1, UserName = "hoge", Password = "1234"},
        new Users{Id = 2, UserName = "piyo", Password = "5678"}
    };
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(Users user, string returnUrl = null)
        {
            const string badUserNameOrPasswordMessage = "Username or password is incorrect.";
            if (user == null)
            {
                return BadRequest(badUserNameOrPasswordMessage);
            }

            // ユーザー名が一致するユーザーを抽出
            var lookupUser = users.Where(u => u.UserName == user.UserName).FirstOrDefault();
            if (lookupUser == null)
            {
                return BadRequest(badUserNameOrPasswordMessage);
            }

            // パスワードの比較
            if (lookupUser?.Password != user.Password)
            {
                return BadRequest(badUserNameOrPasswordMessage);
            }

            // Cookies 認証スキームで新しい ClaimsIdentity を作成し、ユーザー名を追加します。
            var identity = new ClaimsIdentity("MyCookieAuthenticationScheme");
            identity.AddClaim(new Claim(ClaimTypes.Name, lookupUser.UserName));

            // クッキー認証スキームと、上の数行で作成されたIDから作成された新しい ClaimsPrincipal を渡します。
            await HttpContext.SignInAsync("MyCookieAuthenticationScheme", new ClaimsPrincipal(identity));

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("MyCookieAuthenticationScheme");

            return RedirectToAction("Index", "Home");
        }
    }
}
