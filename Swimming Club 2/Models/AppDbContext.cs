using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Swimming_Club_2.Models
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }      
        public DbSet<Swimmer> Swimmers { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }

    }       
    
}
