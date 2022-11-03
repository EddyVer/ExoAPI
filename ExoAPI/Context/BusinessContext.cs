using ExoAPI.Entitie;
using Microsoft.EntityFrameworkCore;

namespace ExoAPI.Context
{
    public class BusinessContext : DbContext
    {
        public DbSet<Product>? Products { get; set; }
       // public DbSet<Entrepot>? Entrepots { get; set; }
        public BusinessContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _configuration.GetConnectionString("MySqlDatabase");
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
        private readonly IConfiguration _configuration;
    }
}
