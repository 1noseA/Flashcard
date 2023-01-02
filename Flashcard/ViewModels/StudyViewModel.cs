using Flashcard.Models;

namespace Flashcard.ViewModels
{
    public class StudyViewModel : CommonViewModel
    {
        
        // 出題リスト
        public List<Words> QuestionList { get; set; }

        // 問題番号
        public int QuestionNo { get; set; }

        // 意味
        public string Meaning { get; set; }

        // 回答
        public string MyAnswer { get; set; }

        // 正答
        public string CorrectAnswer { get; set; }
        
        // 判定
        public string Judgement { get; set; }

        // 正解数
        public int CorrectAnswerCount { get; set; }
    }
}
