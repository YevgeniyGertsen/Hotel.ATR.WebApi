using Hotel.ATR.WebApi.Model;
using Microsoft.EntityFrameworkCore;

namespace Hotel.ATR.WebApi
{
    public class AppDbContex : DbContext
    {
        public AppDbContex(DbContextOptions<AppDbContex> options)
            :base(options)
        {
            
        }

        public DbSet<Team> Teams { get; set; }
    }
}
