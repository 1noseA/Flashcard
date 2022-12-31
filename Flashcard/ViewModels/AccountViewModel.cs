using System.ComponentModel.DataAnnotations;

namespace Flashcard.Models
{
    public class AccountViewModel : CommonViewModel
    {
        // ユーザ名
        [Display(Name = "ユーザ名")]
        [Required(ErrorMessage = "ユーザ名を入力してください")]
        public string UserName { get; set; }

        // パスワード
        [Display(Name = "パスワード")]
        [Required(ErrorMessage = "パスワードを入力してください")]
        public string Password { get; set; }
    }
}
