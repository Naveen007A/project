using Microsoft.EntityFrameworkCore;
using scopeindia.Models;
namespace scopeindia.Entity
{
    public class EntDbContext :DbContext
    {
        public EntDbContext(DbContextOptions options):base(options)
        { 
        }
        public DbSet<Reg1> Entry {  get; set; }
    }
}
