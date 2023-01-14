using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flashcard.Models
{
    [Table("histories")]
    public class Histories
    {
        // 履歴ID
        [Key]
        [Column("history_id")]
        public int HistoryId { get; set; }

        // ユーザID(FK)
        [Column("user_id")]
        public int UserId { get; set; }

        // 学習日
        [Column("study_date")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateOnly StudyDate { get; set; }

        // 出題数
        [Column("study_count")]
        public int StudyCount { get; set; }

        // 正答数
        [Column("correct_answer_count")]
        public int CorrectAnswerCount { get; set; }

        // レコード作成者
        [Column("created_by")]
        public string CreatedBy { get; set; }

        // レコード作成日
        [Column("created_at", TypeName = "timestamp(0)")]
        public DateTime? CreatedAt { get; set; }

        // レコード更新者
        [Column("updated_by")]
        public string UpdatedBy { get; set; }

        // レコード更新日
        [Column("updated_at", TypeName = "timestamp(0)")]
        public DateTime? UpdatedAt { get; set; }

        // ナビゲーションプロパティ
        public Users Users { get; set; }
    }
}
