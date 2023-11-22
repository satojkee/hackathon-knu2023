using Microsoft.EntityFrameworkCore;
using Hackathon.Models;

namespace Hackathon.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
