using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flashcard.Models
{
    [Table("users")]
    public class Users
    {
        // ユーザID
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }

        // ユーザ名
        [Column("user_name")]
        public string UserName { get; set; }

        // パスワード
        [Column("password")]
        public string Password { get; set; }

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
        public List<Words> Words { get; set; }

        public List<Histories> Histories { get; set; }
    }
}
