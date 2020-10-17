using Microsoft.EntityFrameworkCore;
using PredictItPriceRecorder.Domain.Model;
using System.Configuration;

namespace PredictItPriceRecorder.Domain
{
    public class PredictItContext : DbContext
    {
        public virtual DbSet<market> markets { get; set; }
        public virtual DbSet<contract> contracts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["PredictItDb"].ConnectionString);
        }
    }
}