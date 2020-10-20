using Microsoft.EntityFrameworkCore;
using PredictItPriceRecorder.Domain.Model;
using System.Configuration;

namespace PredictItPriceRecorder.Domain
{
    public class PredictItContext : DbContext
    {
        public virtual DbSet<market> markets { get; set; }
        public virtual DbSet<contract> contracts { get; set; }
        public virtual DbSet<contract_price> contract_prices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["PredictItDb"].ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<contract_price>()
                .HasKey(cp => new { cp.time_stamp, cp.contract_id });
        }
    }
}