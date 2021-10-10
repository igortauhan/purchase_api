using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace purchase_api.model
{
    public class PurchaseContext : DbContext
    {
        public DbSet<Purchase> Purchases { get; set; }

        public PurchaseContext(DbContextOptions<PurchaseContext> options) : base(options)
        {
        }
    }
}
