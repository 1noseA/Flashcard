using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flashcard.Models
{
    [Table("words")]
    public class Words
    {
        // 単語ID
        [Key]
        [Column("word_id")]
        public int WordId { get; set; }

        // 単語
        [Column("word")]
        [Display(Name = "単語")]
        [Required(ErrorMessage = "単語を入力してください")]
        public string Word { get; set; }

        // 意味
        [Column("meaning")]
        [Display(Name = "意味")]
        [Required(ErrorMessage = "意味を入力してください")]
        public string Meaning { get; set; }

        // ユーザID(FK)
        [Column("user_id")]
        public int UserId { get; set; }

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

        // ナビゲーションプロパティ
        public Users Users { get; set; }

        public List<Histories> Histories { get; set; }
    }
}
