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

            // seed data
            modelBuilder.Entity<Users>().HasData(new Users { UserId = 1, UserName = "user1", Password="password1" });
            modelBuilder.Entity<Users>().HasData(new Users { UserId = 2, UserName = "user2", Password = "password2" });

            modelBuilder.Entity<Words>().HasData(
                new Words { UserId = 1, WordId = 1, Word = "word1", Meaning = "meaning1" });
            modelBuilder.Entity<Words>().HasData(
                new Words { UserId = 1, WordId = 2, Word = "word2", Meaning = "meaning2" });
            modelBuilder.Entity<Words>().HasData(
                new Words { UserId = 1, WordId = 3, Word = "word3", Meaning = "meaning3" });
            modelBuilder.Entity<Words>().HasData(
                new Words { UserId = 1, WordId = 4, Word = "word4", Meaning = "meaning4" });
            modelBuilder.Entity<Words>().HasData(
                new Words { UserId = 1, WordId = 5, Word = "word5", Meaning = "meaning5" });

        }
    }
}
