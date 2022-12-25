using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Flashcard.Models;

namespace Flashcard.Data
{
    public class FlashcardContext : DbContext
    {
        public FlashcardContext (DbContextOptions<FlashcardContext> options)
            : base(options)
        {
        }

        public DbSet<Flashcard.Models.Words> Words { get; set; } = default!;
    }
}
