using System.ComponentModel.DataAnnotations.Schema;

namespace Flashcard.ViewModels
{
    public class CommonViewModel
    {
        // エラーメッセージ
        public string ErrorMsg { get; set; }

        // メッセージ区分
        public string MsgClass { get; set; }
    }
}
