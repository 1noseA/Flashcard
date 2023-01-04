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

        // 学習日時
        [Column("study_date", TypeName = "timestamp(0)")]
        [DisplayFormat(DataFormatString = "{0:YYYY/MM/DD HH24:MI:SS}", ApplyFormatInEditMode = true)]
        public DateTime StudyDate { get; set; }

        // 出題数
        [Column("stydy_count")]
        public int StydyCount { get; set; }

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
