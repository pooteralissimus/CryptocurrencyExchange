using DbAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccessLibrary.DataAccess
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options) { }
        public DbSet<UserBalance> UserBalance { get; set; }
        public DbSet<CryptoOpenPrices> OpenPrices { get; set; }
    }
}
