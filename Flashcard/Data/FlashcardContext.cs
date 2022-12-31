using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Flashcard.Models;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;
using NuGet.Packaging.Signing;

namespace Flashcard.Data
{
    public class FlashcardContext : DbContext
    {
        public FlashcardContext (DbContextOptions<FlashcardContext> options)
            : base(options)
        {
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

            modelBuilder.Entity<Histories>()
                .HasOne(histories => histories.Words)
                .WithMany(words => words.Histories)
                .HasForeignKey(histories => histories.WordId);
        }
    }
}
