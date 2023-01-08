namespace Flashcard.ViewModels
{
    public class HistoriesViewModel : CommonViewModel
    {

        // ユーザ名
        public string UserName { get; set; }

        // 学習日
        public string[] StudyDate { get; set; }

        // 正答数
        public double?[] CorrectAnswerCount { get; set; }

        // 正答率
        public double?[] CorrectAnswerRate { get; set; }

    }
}
