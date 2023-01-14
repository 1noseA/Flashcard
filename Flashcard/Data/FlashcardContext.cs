using Microsoft.EntityFrameworkCore;
using Flashcard.Models;

namespace Flashcard.Data
{
    public class FlashcardContext : DbContext
    {
        public FlashcardContext (DbContextOptions<FlashcardContext> options)
            : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<Words> Words { get; set; }

        public DbSet<Users> Users { get; set; }

        public DbSet<Histories> Histories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Wordsの設定
            modelBuilder.Entity<Words>()
              .HasOne(words => words.Users) // Usersを参照する。
              .WithMany(users => users.Words) // Userに対し、Wordsは複数存在する。
              .HasForeignKey(users => new { users.UserId }); // 外部制約キーの指定

            // Historiesの設定
            modelBuilder.Entity<Histories>()
                .HasOne(histories => histories.Users)
                .WithMany(users => users.Histories)
                .HasForeignKey(histories => histories.UserId);

            // seed data
            modelBuilder.Entity<Users>().HasData(new Users { UserId = 1, UserName = "user1", Password = "password1" });
            modelBuilder.Entity<Users>().HasData(new Users { UserId = 2, UserName = "user2", Password = "password2" });
            modelBuilder.Entity<Users>().HasData(new Users { UserId = 3, UserName = "user3", Password = "password3" });

            // user1の単語
            modelBuilder.Entity<Words>().HasData(
                new Words { UserId = 1, WordId = 1, Word = "ゲートウェイ", Meaning = "OSI基本参照モデルの7層すべてを認識するが、主にトランスポート層以上でプロトコルの異なるネットワーク同士を接続する役割を持つ装置。" });
            modelBuilder.Entity<Words>().HasData(
                new Words { UserId = 1, WordId = 2, Word = "サニタイジング", Meaning = "ユーザの入力値を受け取り処理するWebアプリケーションにおいて、入力データ中のスクリプトやコマンドとして特別な意味を持つ文字があった場合、HTML出力やコマンド発行の直前でエスケープ処理し無害化すること。" });
            modelBuilder.Entity<Words>().HasData(
                new Words { UserId = 1, WordId = 3, Word = "サブネットマスク", Meaning = "IPアドレスをネットワークアドレスとホストアドレスに分割し，複数のより小さいネットワークを形成するために使用する32ビット(IPv4では)のビット列。" });
            modelBuilder.Entity<Words>().HasData(
                new Words { UserId = 1, WordId = 4, Word = "スタック", Meaning = "後入先出し方式でデータを入出力する記憶構造のこと。サブルーチン終了後の戻りアドレスや局所変数などを保持するのに使用される。" });
            modelBuilder.Entity<Words>().HasData(
                new Words { UserId = 1, WordId = 5, Word = "ソーシャルエンジニアリング", Meaning = "技術的な方法ではなく人の心理的な弱みに付け込んで、パスワードなどを不正に取得する方法。" });
            modelBuilder.Entity<Words>().HasData(
                new Words { UserId = 1, WordId = 6, Word = "ターンアラウンドタイム", Meaning = "利用者がシステムに処理要求を行ってから、すべての結果の出力が終了するまでの時間のこと。" });
            modelBuilder.Entity<Words>().HasData(
                new Words { UserId = 1, WordId = 7, Word = "ディジタルディバイド", Meaning = "パソコンやインターネットなどの情報通信技術を使いこなせる者と使いこなせない者の間に生じる、待遇や貧富、機会の格差のこと。" });
            modelBuilder.Entity<Words>().HasData(
                new Words { UserId = 1, WordId = 8, Word = "パリティチェック", Meaning = "データ通信で伝送時の誤りを検出する最もシンプルな方法の一つである。送信するデータの一定長のビット列に1ビットの検査ビットを付加し、受信側では受信データとパリティビットを照合することで誤りを検出する。" });
            modelBuilder.Entity<Words>().HasData(
                new Words { UserId = 1, WordId = 9, Word = "フェールセーフ", Meaning = "システムの不具合や故障が発生した場合に、障害の影響範囲を最小限にとどめ、常に安全を最優先にして制御を行う考え方。" });
            modelBuilder.Entity<Words>().HasData(
                new Words { UserId = 1, WordId = 10, Word = "フラグメンテーション", Meaning = "主記憶やハードディスクのような記憶装置において不連続な未使用領域が生じる現象、またはその領域のこと。" });
            modelBuilder.Entity<Words>().HasData(
                new Words { UserId = 1, WordId = 11, Word = "ブルートフォース攻撃", Meaning = "特定の文字数、および、文字種で設定される可能性のあるすべての組合せを試すことで不正ログインやパスワード解析を試みる攻撃手法で、総当たり攻撃とも呼ばれる。" });
            modelBuilder.Entity<Words>().HasData(
                new Words { UserId = 1, WordId = 12, Word = "メモリインタリーブ", Meaning = "メモリアクセス高速化のための技法で、物理上ひとつの主記憶を同時アクセス可能な複数の論理的な領域(バンク)に分け、これに並列アクセスすることで見かけ上のアクセス時間を短縮することができる。" });
            modelBuilder.Entity<Words>().HasData(
                new Words { UserId = 1, WordId = 13, Word = "cookie", Meaning = "Webサーバに対するアクセスがどのPCからのものであるかを識別するために，WebサーバやWebページの指示によってWebブラウザにユーザ情報などを保存する仕組み。" });
            modelBuilder.Entity<Words>().HasData(
                new Words { UserId = 1, WordId = 14, Word = "DNS", Meaning = "ドメイン名・ホスト名とIPアドレスを結びつけて変換する(名前解決する)仕組み。" });
            modelBuilder.Entity<Words>().HasData(
                new Words { UserId = 1, WordId = 15, Word = "DRAM", Meaning = "コンデンサに電荷を蓄えることにより情報を記憶し、電源供給が無くなると記憶情報も失われる揮発性メモリ。集積度を上げることが比較的簡単にできるためコンピュータの主記憶装置として使用されている。" });
            modelBuilder.Entity<Words>().HasData(
                new Words { UserId = 1, WordId = 16, Word = "DoS攻撃", Meaning = "通常ではありえない数のリクエストをサーバに送信することでサーバやネットワーク回線を過負荷状態にし、サーバのシステムダウンや応答停止などの障害を引き起こさせる攻撃手法。" });
            modelBuilder.Entity<Words>().HasData(
                new Words { UserId = 1, WordId = 17, Word = "FIFO", Meaning = "置き換え対象の中に最も古くから存在するページを追い出す\"先入れ先出し\"のアルゴリズム。" });
            modelBuilder.Entity<Words>().HasData(
                new Words { UserId = 1, WordId = 18, Word = "LRU", Meaning = "読み込まれてから最も長い時間参照されていないものを置き換え対象とするアルゴリズム。" });
            modelBuilder.Entity<Words>().HasData(
                new Words { UserId = 1, WordId = 19, Word = "MACアドレス", Meaning = "ネットワーク上の機器を識別するために、各機器に一意に割り当てられた6バイト(48ビット)の番号のこと。" });
            modelBuilder.Entity<Words>().HasData(
                new Words { UserId = 1, WordId = 20, Word = "MTBF", Meaning = "システムの修理が完了し正常に稼働し始めてから、次回故障するまでの平均故障間隔のこと。" });
            modelBuilder.Entity<Words>().HasData(
                new Words { UserId = 1, WordId = 21, Word = "NAT", Meaning = "企業や組織のネットワーク内で割り当てられているプライベートIPアドレスとインターネット上でのアドレスであるグローバルIPアドレスを1対1で相互に変換する技術。" });
            modelBuilder.Entity<Words>().HasData(
                new Words { UserId = 1, WordId = 22, Word = "SRAM", Meaning = "フリップフロップと呼ばれる回路を用いることで、DRAMのようなリフレッシュ動作の必要がない非常に高速に動作する半導体メモリ。DRAMと比べて記憶容量あたりの単価が高いため、容量が少ないキャッシュメモリなどに使用されている。" });

            // user2の単語
            modelBuilder.Entity<Words>().HasData(
                new Words { UserId = 2, WordId = 23, Word = "データディクショナリ", Meaning = "Oracle内部の管理情報を格納する特殊な表。" });
            modelBuilder.Entity<Words>().HasData(
                new Words { UserId = 2, WordId = 24, Word = "DUAL表", Meaning = "データベース作成時に自動的に作成されるVARCHAR2型のDUMMYという名前の列を持ち、データを1行だけ含む表。" });
            modelBuilder.Entity<Words>().HasData(
                new Words { UserId = 2, WordId = 25, Word = "外部表", Meaning = "OSファイルシステム上のファイルをあたかもOracleデータベースにある表のように扱うことができる。" });
            modelBuilder.Entity<Words>().HasData(
                new Words { UserId = 2, WordId = 26, Word = "シノニム", Meaning = "オブジェクトにつける別名。" });
            modelBuilder.Entity<Words>().HasData(
                new Words { UserId = 2, WordId = 27, Word = "オプティマイザ", Meaning = "SQLを実行するにあたり、どのようなオブジェクトにどのような手順でアクセスすれば効率的かを判断するOracleの内部コンポーネント。" });

            // 履歴
            modelBuilder.Entity<Histories>().HasData(
                new Histories { HistoryId = 1, UserId = 1, StudyDate = new DateTime(2022, 12, 30), StudyCount = 10, CorrectAnswerCount = 4 });
            modelBuilder.Entity<Histories>().HasData(
                new Histories { HistoryId = 2, UserId = 1, StudyDate = new DateTime(2022, 12, 31), StudyCount = 10, CorrectAnswerCount = 5 });
            modelBuilder.Entity<Histories>().HasData(
                new Histories { HistoryId = 3, UserId = 1, StudyDate = new DateTime(2023, 1, 1), StudyCount = 10, CorrectAnswerCount = 6 });
            modelBuilder.Entity<Histories>().HasData(
                new Histories { HistoryId = 4, UserId = 1, StudyDate = new DateTime(2023, 1, 2), StudyCount = 10, CorrectAnswerCount = 7 });
            modelBuilder.Entity<Histories>().HasData(
                new Histories { HistoryId = 5, UserId = 1, StudyDate = new DateTime(2023, 1, 3), StudyCount = 10, CorrectAnswerCount = 8 });
            modelBuilder.Entity<Histories>().HasData(
                new Histories { HistoryId = 6, UserId = 2, StudyDate = new DateTime(2023, 1, 3), StudyCount = 5, CorrectAnswerCount = 4 });
            modelBuilder.Entity<Histories>().HasData(
                new Histories { HistoryId = 7, UserId = 1, StudyDate = new DateTime(2023, 1, 4), StudyCount = 20, CorrectAnswerCount = 18 });
        }
    }
}
