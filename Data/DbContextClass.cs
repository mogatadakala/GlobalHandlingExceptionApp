using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using GlobalExceptionHandling.Models;

namespace GlobalExceptionHandling.Data
{
    public class DbContextClass:DbContext
    {
      //  protected readonly IConfiguration configuration;
        public DbContextClass(DbContextOptions<DbContextClass> options) : base(options)
        {
           
        }
        public DbSet<Product> products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(this.configuration.GetConnectionString("GlobalExcept"));
        //}
    }
}
