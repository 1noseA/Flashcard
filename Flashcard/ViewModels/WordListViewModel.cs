using Flashcard.Models;
using System.ComponentModel.DataAnnotations;

namespace Flashcard.ViewModels
{
    public class WordListViewModel : CommonViewModel
    {
        //// 単語
        //[Display(Name = "単語")]
        //[Required(ErrorMessage = "単語を入力してください")]
        //public string Word { get; set; }

        //// 意味
        //[Display(Name = "意味")]
        //[Required(ErrorMessage = "意味を入力してください")]
        //public string Meaning { get; set; }

        //// ユーザID
        //[Display(Name = "ユーザID")]
        //public int UserId { get; set; }

        // 単語
        public Words Words { get; set; }

        // 単語リスト
        public List<Words> WordList { get; set; }
    }
}
