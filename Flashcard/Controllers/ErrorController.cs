using Microsoft.AspNetCore.Mvc;
using Flashcard.Data;
using System.Security.Claims;
using Flashcard.ViewModels;
using Newtonsoft.Json;
using Flashcard.Models;
using MessagePack;
using Microsoft.AspNetCore.Authentication;
using System.Security.Principal;

namespace Flashcard.Controllers
{
    public class ErrorController : Controller
    {
        CommonViewModel viewModel = new CommonViewModel();

        // 初期処理
        // GET: Error
        public async Task<IActionResult> Error()
        {
            if (TempData["ErrorMsg"] != null)
            {
                viewModel.ErrorMsg = (string)TempData["ErrorMsg"];
            }
            else
            {
                viewModel.ErrorMsg = "一定時間操作がなかったため、タイムアウトしました。再度ログインし直してください。";
            }

            // ログアウトする
            await HttpContext.SignOutAsync("MyCookieAuthenticationScheme");

            // Userを上書き
            HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);

            return View(viewModel);
        }
    }
}
