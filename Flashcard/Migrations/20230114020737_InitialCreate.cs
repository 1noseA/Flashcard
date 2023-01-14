using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Flashcard.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_name = table.Column<string>(type: "text", nullable: true),
                    password = table.Column<string>(type: "text", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "histories",
                columns: table => new
                {
                    history_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    study_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    study_count = table.Column<int>(type: "integer", nullable: false),
                    correct_answer_count = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_histories", x => x.history_id);
                    table.ForeignKey(
                        name: "FK_histories_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "words",
                columns: table => new
                {
                    word_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    word = table.Column<string>(type: "text", nullable: false),
                    meaning = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_words", x => x.word_id);
                    table.ForeignKey(
                        name: "FK_words_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "user_id", "created_at", "created_by", "password", "updated_at", "updated_by", "user_name" },
                values: new object[,]
                {
                    { 1, null, null, "password1", null, null, "user1" },
                    { 2, null, null, "password2", null, null, "user2" },
                    { 3, null, null, "password3", null, null, "user3" }
                });

            migrationBuilder.InsertData(
                table: "histories",
                columns: new[] { "history_id", "correct_answer_count", "created_at", "created_by", "study_count", "study_date", "updated_at", "updated_by", "user_id" },
                values: new object[,]
                {
                    { 1, 4, null, null, 10, new DateTime(2022, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 1 },
                    { 2, 5, null, null, 10, new DateTime(2022, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 1 },
                    { 3, 6, null, null, 10, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 1 },
                    { 4, 7, null, null, 10, new DateTime(2023, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 1 },
                    { 5, 8, null, null, 10, new DateTime(2023, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 1 },
                    { 6, 4, null, null, 5, new DateTime(2023, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 2 },
                    { 7, 18, null, null, 20, new DateTime(2023, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 1 }
                });

            migrationBuilder.InsertData(
                table: "words",
                columns: new[] { "word_id", "created_at", "created_by", "meaning", "updated_at", "updated_by", "user_id", "word" },
                values: new object[,]
                {
                    { 1, null, null, "OSI基本参照モデルの7層すべてを認識するが、主にトランスポート層以上でプロトコルの異なるネットワーク同士を接続する役割を持つ装置。", null, null, 1, "ゲートウェイ" },
                    { 2, null, null, "ユーザの入力値を受け取り処理するWebアプリケーションにおいて、入力データ中のスクリプトやコマンドとして特別な意味を持つ文字があった場合、HTML出力やコマンド発行の直前でエスケープ処理し無害化すること。", null, null, 1, "サニタイジング" },
                    { 3, null, null, "IPアドレスをネットワークアドレスとホストアドレスに分割し，複数のより小さいネットワークを形成するために使用する32ビット(IPv4では)のビット列。", null, null, 1, "サブネットマスク" },
                    { 4, null, null, "後入先出し方式でデータを入出力する記憶構造のこと。サブルーチン終了後の戻りアドレスや局所変数などを保持するのに使用される。", null, null, 1, "スタック" },
                    { 5, null, null, "技術的な方法ではなく人の心理的な弱みに付け込んで、パスワードなどを不正に取得する方法。", null, null, 1, "ソーシャルエンジニアリング" },
                    { 6, null, null, "利用者がシステムに処理要求を行ってから、すべての結果の出力が終了するまでの時間のこと。", null, null, 1, "ターンアラウンドタイム" },
                    { 7, null, null, "パソコンやインターネットなどの情報通信技術を使いこなせる者と使いこなせない者の間に生じる、待遇や貧富、機会の格差のこと。", null, null, 1, "ディジタルディバイド" },
                    { 8, null, null, "データ通信で伝送時の誤りを検出する最もシンプルな方法の一つである。送信するデータの一定長のビット列に1ビットの検査ビットを付加し、受信側では受信データとパリティビットを照合することで誤りを検出する。", null, null, 1, "パリティチェック" },
                    { 9, null, null, "システムの不具合や故障が発生した場合に、障害の影響範囲を最小限にとどめ、常に安全を最優先にして制御を行う考え方。", null, null, 1, "フェールセーフ" },
                    { 10, null, null, "主記憶やハードディスクのような記憶装置において不連続な未使用領域が生じる現象、またはその領域のこと。", null, null, 1, "フラグメンテーション" },
                    { 11, null, null, "特定の文字数、および、文字種で設定される可能性のあるすべての組合せを試すことで不正ログインやパスワード解析を試みる攻撃手法で、総当たり攻撃とも呼ばれる。", null, null, 1, "ブルートフォース攻撃" },
                    { 12, null, null, "メモリアクセス高速化のための技法で、物理上ひとつの主記憶を同時アクセス可能な複数の論理的な領域(バンク)に分け、これに並列アクセスすることで見かけ上のアクセス時間を短縮することができる。", null, null, 1, "メモリインタリーブ" },
                    { 13, null, null, "Webサーバに対するアクセスがどのPCからのものであるかを識別するために，WebサーバやWebページの指示によってWebブラウザにユーザ情報などを保存する仕組み。", null, null, 1, "cookie" },
                    { 14, null, null, "ドメイン名・ホスト名とIPアドレスを結びつけて変換する(名前解決する)仕組み。", null, null, 1, "DNS" },
                    { 15, null, null, "コンデンサに電荷を蓄えることにより情報を記憶し、電源供給が無くなると記憶情報も失われる揮発性メモリ。集積度を上げることが比較的簡単にできるためコンピュータの主記憶装置として使用されている。", null, null, 1, "DRAM" },
                    { 16, null, null, "通常ではありえない数のリクエストをサーバに送信することでサーバやネットワーク回線を過負荷状態にし、サーバのシステムダウンや応答停止などの障害を引き起こさせる攻撃手法。", null, null, 1, "DoS攻撃" },
                    { 17, null, null, "置き換え対象の中に最も古くから存在するページを追い出す\"先入れ先出し\"のアルゴリズム。", null, null, 1, "FIFO" },
                    { 18, null, null, "読み込まれてから最も長い時間参照されていないものを置き換え対象とするアルゴリズム。", null, null, 1, "LRU" },
                    { 19, null, null, "ネットワーク上の機器を識別するために、各機器に一意に割り当てられた6バイト(48ビット)の番号のこと。", null, null, 1, "MACアドレス" },
                    { 20, null, null, "システムの修理が完了し正常に稼働し始めてから、次回故障するまでの平均故障間隔のこと。", null, null, 1, "MTBF" },
                    { 21, null, null, "企業や組織のネットワーク内で割り当てられているプライベートIPアドレスとインターネット上でのアドレスであるグローバルIPアドレスを1対1で相互に変換する技術。", null, null, 1, "NAT" },
                    { 22, null, null, "フリップフロップと呼ばれる回路を用いることで、DRAMのようなリフレッシュ動作の必要がない非常に高速に動作する半導体メモリ。DRAMと比べて記憶容量あたりの単価が高いため、容量が少ないキャッシュメモリなどに使用されている。", null, null, 1, "SRAM" },
                    { 23, null, null, "Oracle内部の管理情報を格納する特殊な表。", null, null, 2, "データディクショナリ" },
                    { 24, null, null, "データベース作成時に自動的に作成されるVARCHAR2型のDUMMYという名前の列を持ち、データを1行だけ含む表。", null, null, 2, "DUAL表" },
                    { 25, null, null, "OSファイルシステム上のファイルをあたかもOracleデータベースにある表のように扱うことができる。", null, null, 2, "外部表" },
                    { 26, null, null, "オブジェクトにつける別名。", null, null, 2, "シノニム" },
                    { 27, null, null, "SQLを実行するにあたり、どのようなオブジェクトにどのような手順でアクセスすれば効率的かを判断するOracleの内部コンポーネント。", null, null, 2, "オプティマイザ" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_histories_user_id",
                table: "histories",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_words_user_id",
                table: "words",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "histories");

            migrationBuilder.DropTable(
                name: "words");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
