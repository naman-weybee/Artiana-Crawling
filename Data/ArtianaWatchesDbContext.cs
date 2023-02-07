using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artiana_Crawling.Data
{
    public class ArtianaWatchesDbContext : DbContext
    {
        public ArtianaWatchesDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"data source=DESKTOP-9J2CV47; database=ArtianaWatches; integrated security=SSPI");
        }

        public DbSet<WatchAuctions> tbl_Watch_Auctions { get; set; }
        public DbSet<WatchDetails> tbl_Watch_Details { get; set; }
    }
}
