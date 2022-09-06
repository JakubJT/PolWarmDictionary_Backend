using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using DAL.Models;

namespace DAL
{

    public class DictionaryContext : DbContext
    {
        public DictionaryContext()
        {
        }
        public DictionaryContext(DbContextOptions<DictionaryContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
    => options.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=dictionary;Trusted_Connection=True");

        public DbSet<Word>? Words { get; set; }
        public DbSet<Author>? Authors { get; set; }
        public DbSet<PartOfSpeech>? PartOfSpeeches { get; set; }
        public DbSet<WordGroup>? WordGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WordGroup>().ToTable(nameof(WordGroup))
                .HasMany(wg => wg.Words)
                .WithMany(w => w.WordGroups);
        }
    }
}

