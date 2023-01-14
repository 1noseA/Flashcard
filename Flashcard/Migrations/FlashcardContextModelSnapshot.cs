﻿// <auto-generated />
using System;
using Flashcard.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Flashcard.Migrations
{
    [DbContext(typeof(FlashcardContext))]
    partial class FlashcardContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Flashcard.Models.Histories", b =>
                {
                    b.Property<int>("HistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("history_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("HistoryId"));

                    b.Property<int>("CorrectAnswerCount")
                        .HasColumnType("integer")
                        .HasColumnName("correct_answer_count");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp(0) without time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<int>("StudyCount")
                        .HasColumnType("integer")
                        .HasColumnName("study_count");

                    b.Property<DateOnly>("StudyDate")
                        .HasColumnType("date")
                        .HasColumnName("study_date");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp(0) without time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text")
                        .HasColumnName("updated_by");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("HistoryId");

                    b.HasIndex("UserId");

                    b.ToTable("histories");

                    b.HasData(
                        new
                        {
                            HistoryId = 1,
                            CorrectAnswerCount = 4,
                            StudyCount = 10,
                            StudyDate = new DateOnly(2022, 12, 30),
                            UserId = 1
                        },
                        new
                        {
                            HistoryId = 2,
                            CorrectAnswerCount = 5,
                            StudyCount = 10,
                            StudyDate = new DateOnly(2022, 12, 31),
                            UserId = 1
                        },
                        new
                        {
                            HistoryId = 3,
                            CorrectAnswerCount = 6,
                            StudyCount = 10,
                            StudyDate = new DateOnly(2023, 1, 1),
                            UserId = 1
                        },
                        new
                        {
                            HistoryId = 4,
                            CorrectAnswerCount = 7,
                            StudyCount = 10,
                            StudyDate = new DateOnly(2023, 1, 2),
                            UserId = 1
                        },
                        new
                        {
                            HistoryId = 5,
                            CorrectAnswerCount = 8,
                            StudyCount = 10,
                            StudyDate = new DateOnly(2023, 1, 3),
                            UserId = 1
                        },
                        new
                        {
                            HistoryId = 6,
                            CorrectAnswerCount = 4,
                            StudyCount = 5,
                            StudyDate = new DateOnly(2023, 1, 3),
                            UserId = 2
                        },
                        new
                        {
                            HistoryId = 7,
                            CorrectAnswerCount = 18,
                            StudyCount = 20,
                            StudyDate = new DateOnly(2023, 1, 4),
                            UserId = 1
                        },
                        new
                        {
                            HistoryId = 8,
                            CorrectAnswerCount = 9,
                            StudyCount = 10,
                            StudyDate = new DateOnly(2023, 1, 14),
                            UserId = 1
                        });
                });

            modelBuilder.Entity("Flashcard.Models.Users", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp(0) without time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<string>("Password")
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp(0) without time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text")
                        .HasColumnName("updated_by");

                    b.Property<string>("UserName")
                        .HasColumnType("text")
                        .HasColumnName("user_name");

                    b.HasKey("UserId");

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Password = "password1",
                            UserName = "user1"
                        },
                        new
                        {
                            UserId = 2,
                            Password = "password2",
                            UserName = "user2"
                        },
                        new
                        {
                            UserId = 3,
                            Password = "password3",
                            UserName = "user3"
                        });
                });

            modelBuilder.Entity("Flashcard.Models.Words", b =>
                {
                    b.Property<int>("WordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("word_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("WordId"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp(0) without time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<string>("Meaning")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("meaning");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp(0) without time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text")
                        .HasColumnName("updated_by");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<string>("Word")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("word");

                    b.HasKey("WordId");

                    b.HasIndex("UserId");

                    b.ToTable("words");

                    b.HasData(
                        new
                        {
                            WordId = 1,
                            Meaning = "OSI基本参照モデルの7層すべてを認識するが、主にトランスポート層以上でプロトコルの異なるネットワーク同士を接続する役割を持つ装置。",
                            UserId = 1,
                            Word = "ゲートウェイ"
                        },
                        new
                        {
                            WordId = 2,
                            Meaning = "ユーザの入力値を受け取り処理するWebアプリケーションにおいて、入力データ中のスクリプトやコマンドとして特別な意味を持つ文字があった場合、HTML出力やコマンド発行の直前でエスケープ処理し無害化すること。",
                            UserId = 1,
                            Word = "サニタイジング"
                        },
                        new
                        {
                            WordId = 3,
                            Meaning = "IPアドレスをネットワークアドレスとホストアドレスに分割し，複数のより小さいネットワークを形成するために使用する32ビット(IPv4では)のビット列。",
                            UserId = 1,
                            Word = "サブネットマスク"
                        },
                        new
                        {
                            WordId = 4,
                            Meaning = "後入先出し方式でデータを入出力する記憶構造のこと。サブルーチン終了後の戻りアドレスや局所変数などを保持するのに使用される。",
                            UserId = 1,
                            Word = "スタック"
                        },
                        new
                        {
                            WordId = 5,
                            Meaning = "技術的な方法ではなく人の心理的な弱みに付け込んで、パスワードなどを不正に取得する方法。",
                            UserId = 1,
                            Word = "ソーシャルエンジニアリング"
                        },
                        new
                        {
                            WordId = 6,
                            Meaning = "利用者がシステムに処理要求を行ってから、すべての結果の出力が終了するまでの時間のこと。",
                            UserId = 1,
                            Word = "ターンアラウンドタイム"
                        },
                        new
                        {
                            WordId = 7,
                            Meaning = "パソコンやインターネットなどの情報通信技術を使いこなせる者と使いこなせない者の間に生じる、待遇や貧富、機会の格差のこと。",
                            UserId = 1,
                            Word = "ディジタルディバイド"
                        },
                        new
                        {
                            WordId = 8,
                            Meaning = "データ通信で伝送時の誤りを検出する最もシンプルな方法の一つである。送信するデータの一定長のビット列に1ビットの検査ビットを付加し、受信側では受信データとパリティビットを照合することで誤りを検出する。",
                            UserId = 1,
                            Word = "パリティチェック"
                        },
                        new
                        {
                            WordId = 9,
                            Meaning = "システムの不具合や故障が発生した場合に、障害の影響範囲を最小限にとどめ、常に安全を最優先にして制御を行う考え方。",
                            UserId = 1,
                            Word = "フェールセーフ"
                        },
                        new
                        {
                            WordId = 10,
                            Meaning = "主記憶やハードディスクのような記憶装置において不連続な未使用領域が生じる現象、またはその領域のこと。",
                            UserId = 1,
                            Word = "フラグメンテーション"
                        },
                        new
                        {
                            WordId = 11,
                            Meaning = "特定の文字数、および、文字種で設定される可能性のあるすべての組合せを試すことで不正ログインやパスワード解析を試みる攻撃手法で、総当たり攻撃とも呼ばれる。",
                            UserId = 1,
                            Word = "ブルートフォース攻撃"
                        },
                        new
                        {
                            WordId = 12,
                            Meaning = "メモリアクセス高速化のための技法で、物理上ひとつの主記憶を同時アクセス可能な複数の論理的な領域(バンク)に分け、これに並列アクセスすることで見かけ上のアクセス時間を短縮することができる。",
                            UserId = 1,
                            Word = "メモリインタリーブ"
                        },
                        new
                        {
                            WordId = 13,
                            Meaning = "Webサーバに対するアクセスがどのPCからのものであるかを識別するために，WebサーバやWebページの指示によってWebブラウザにユーザ情報などを保存する仕組み。",
                            UserId = 1,
                            Word = "cookie"
                        },
                        new
                        {
                            WordId = 14,
                            Meaning = "ドメイン名・ホスト名とIPアドレスを結びつけて変換する(名前解決する)仕組み。",
                            UserId = 1,
                            Word = "DNS"
                        },
                        new
                        {
                            WordId = 15,
                            Meaning = "コンデンサに電荷を蓄えることにより情報を記憶し、電源供給が無くなると記憶情報も失われる揮発性メモリ。集積度を上げることが比較的簡単にできるためコンピュータの主記憶装置として使用されている。",
                            UserId = 1,
                            Word = "DRAM"
                        },
                        new
                        {
                            WordId = 16,
                            Meaning = "通常ではありえない数のリクエストをサーバに送信することでサーバやネットワーク回線を過負荷状態にし、サーバのシステムダウンや応答停止などの障害を引き起こさせる攻撃手法。",
                            UserId = 1,
                            Word = "DoS攻撃"
                        },
                        new
                        {
                            WordId = 17,
                            Meaning = "置き換え対象の中に最も古くから存在するページを追い出す\"先入れ先出し\"のアルゴリズム。",
                            UserId = 1,
                            Word = "FIFO"
                        },
                        new
                        {
                            WordId = 18,
                            Meaning = "読み込まれてから最も長い時間参照されていないものを置き換え対象とするアルゴリズム。",
                            UserId = 1,
                            Word = "LRU"
                        },
                        new
                        {
                            WordId = 19,
                            Meaning = "ネットワーク上の機器を識別するために、各機器に一意に割り当てられた6バイト(48ビット)の番号のこと。",
                            UserId = 1,
                            Word = "MACアドレス"
                        },
                        new
                        {
                            WordId = 20,
                            Meaning = "システムの修理が完了し正常に稼働し始めてから、次回故障するまでの平均故障間隔のこと。",
                            UserId = 1,
                            Word = "MTBF"
                        },
                        new
                        {
                            WordId = 21,
                            Meaning = "企業や組織のネットワーク内で割り当てられているプライベートIPアドレスとインターネット上でのアドレスであるグローバルIPアドレスを1対1で相互に変換する技術。",
                            UserId = 1,
                            Word = "NAT"
                        },
                        new
                        {
                            WordId = 22,
                            Meaning = "フリップフロップと呼ばれる回路を用いることで、DRAMのようなリフレッシュ動作の必要がない非常に高速に動作する半導体メモリ。DRAMと比べて記憶容量あたりの単価が高いため、容量が少ないキャッシュメモリなどに使用されている。",
                            UserId = 1,
                            Word = "SRAM"
                        },
                        new
                        {
                            WordId = 23,
                            Meaning = "Oracle内部の管理情報を格納する特殊な表。",
                            UserId = 2,
                            Word = "データディクショナリ"
                        },
                        new
                        {
                            WordId = 24,
                            Meaning = "データベース作成時に自動的に作成されるVARCHAR2型のDUMMYという名前の列を持ち、データを1行だけ含む表。",
                            UserId = 2,
                            Word = "DUAL表"
                        },
                        new
                        {
                            WordId = 25,
                            Meaning = "OSファイルシステム上のファイルをあたかもOracleデータベースにある表のように扱うことができる。",
                            UserId = 2,
                            Word = "外部表"
                        },
                        new
                        {
                            WordId = 26,
                            Meaning = "オブジェクトにつける別名。",
                            UserId = 2,
                            Word = "シノニム"
                        },
                        new
                        {
                            WordId = 27,
                            Meaning = "SQLを実行するにあたり、どのようなオブジェクトにどのような手順でアクセスすれば効率的かを判断するOracleの内部コンポーネント。",
                            UserId = 2,
                            Word = "オプティマイザ"
                        },
                        new
                        {
                            WordId = 28,
                            Meaning = "meaning1",
                            UserId = 3,
                            Word = "word1"
                        },
                        new
                        {
                            WordId = 29,
                            Meaning = "meaning2",
                            UserId = 3,
                            Word = "word2"
                        },
                        new
                        {
                            WordId = 30,
                            Meaning = "meaning3",
                            UserId = 3,
                            Word = "word3"
                        });
                });

            modelBuilder.Entity("Flashcard.Models.Histories", b =>
                {
                    b.HasOne("Flashcard.Models.Users", "Users")
                        .WithMany("Histories")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Flashcard.Models.Words", b =>
                {
                    b.HasOne("Flashcard.Models.Users", "Users")
                        .WithMany("Words")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Flashcard.Models.Users", b =>
                {
                    b.Navigation("Histories");

                    b.Navigation("Words");
                });
#pragma warning restore 612, 618
        }
    }
}
