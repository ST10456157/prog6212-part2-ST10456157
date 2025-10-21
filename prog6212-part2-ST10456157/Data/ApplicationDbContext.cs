using System.Collections.Generic;
using System.Security.Claims;
namespace prog6212_part2_ST10456157.Models
{

    namespace prog6212_part2_ST10456157.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Claim> Claims { get; set; }
    }
}
