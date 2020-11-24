using Microsoft.EntityFrameworkCore;

namespace Rodrigo.Tech.Models.Database.DB
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        #region Tables
        public DbSet<object> EmailsTable { get; set; }
        #endregion
    }
}
