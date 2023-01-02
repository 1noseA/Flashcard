using Flashcard.Models;
using System.ComponentModel.DataAnnotations;

namespace Flashcard.ViewModels
{
    public class WordListViewModel : CommonViewModel
    {
        // 単語
        public Words Words { get; set; }

        // 単語リスト
        public List<Words> WordList { get; set; }
    }
}
