using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artiana_Crawling.Data
{
    public class WatchAuctions
    {
        [Key]
        public string Id { get; set; }
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
        public string? Link { get; set; }
        public string? SaleNumber { get; set; }
        public string? StartDate { get; set; }
        public string? StartMonth { get; set; }
        public string? StartYear { get; set; }
        public string? EndDate { get; set; }
        public string? EndMonth { get; set; }
        public string? EndYear { get; set; }
        public string? EndTime { get; set; }
        public string? CountryTimeZone { get; set; }
        public string? AuctionStatus { get; set; }
    }
}
