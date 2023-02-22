using Microsoft.EntityFrameworkCore;
using WebApiAuthExample.Models;

namespace WebApiAuthExample.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<UserSecretModel> UserSercrets { get; set; }
    }
}
