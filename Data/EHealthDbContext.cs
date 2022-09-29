using HealthcareApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthcareApi.Data
{
    public class EHealthDbContext : DbContext
    {
        public EHealthDbContext(DbContextOptions<EHealthDbContext>options) :base(options)
        {

        }

        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PaymentInfo> PaymentInfos { get; set; }    

      
    }
}
