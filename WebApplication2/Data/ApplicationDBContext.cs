using Microsoft.EntityFrameworkCore;
using WebApplication2.Models.Entity;

namespace WebApplication2.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
         : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }


    }
}
