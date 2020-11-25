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
                Id = Guid.Parse("282788b1-fada-4986-9907-db48205b2194"),
                Name = "English"
            }, new Language
            {
                Id = Guid.Parse("fc8b0f13-004a-431e-a8c5-51d68387f77a"),
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
