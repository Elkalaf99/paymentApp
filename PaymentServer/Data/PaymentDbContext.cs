using Microsoft.EntityFrameworkCore;
using PaymentProject.Models;

namespace PaymentProject.Data
{
    public class PaymentDbContext : DbContext
    {
        public PaymentDbContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {

        }
        public DbSet<PaymentDetail> PaymentDetails { get; set; }
    }
}
