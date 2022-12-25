using System.ComponentModel.DataAnnotations.Schema;

namespace Flashcard.Models
{
    [Table("histories")]
    public class Histories
    {
        // ID
        [Column("id")]
        public int Id { get; set; }

        // ユーザID
        [Column("user_id")]
        public int UserId { get; set; }

        // 単語ID
        [Column("word_id")]
        public int WordId { get; set; }

        // 回答日
        [Column("study_date")]
        public DateTime study_date { get; set; }

        // 出題数
        [Column("stydy_count")]
        public int stydy_count { get; set; }

        // 正解数
        [Column("correct_answer_count")]
        public int correct_answer_count { get; set; }

        // レコード作成者
        [Column("created_by")]
        public string CreatedBy { get; set; }

        // レコード作成日
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        // レコード更新者
        [Column("updated_by")]
        public string UpdatedBy { get; set; }

        // レコード更新日
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
