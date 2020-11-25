using Microsoft.EntityFrameworkCore;
using Rodrigo.Tech.Repository.Tables;
using System;

namespace Rodrigo.Tech.Repository.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>().HasData(new Language
            {
                Id = Guid.NewGuid(),
                Name = "English"
            }, new Language
            {
                Id = Guid.NewGuid(),
                Name = "Spanish"
            });

            modelBuilder.Entity<Email>()
                .HasIndex(x => x.EmailAddress).IsUnique();

            modelBuilder.Entity<EmailBody>()
                .HasIndex(x => x.LanguageId).IsUnique();

            modelBuilder.Entity<Language>()
                .HasIndex(x => x.Name).IsUnique();
        }

        #region Tables
        public DbSet<Email> Emails { get; set; }

        public DbSet<EmailBody> EmailBodies {get; set;}

        public DbSet<Language> Languages { get; set; }
        #endregion
    }
}
