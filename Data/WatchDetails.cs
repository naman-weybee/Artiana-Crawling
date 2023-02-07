using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artiana_Crawling.Data
{
    public class WatchDetails
    {
        [Key]
        public int Id { get; set; }
        public WatchAuctions Auction { get; set; }
        public string AuctionId { get; set; }
        public string? ImageUrl { get; set; }
        public string? WatchArtist { get; set; }
        public string? WatchPaintingName { get; set; }
        public string? RefrenceNo { get; set; }
        public string? Circa { get; set; }
        public string? CaseMaterial { get; set; }
        public string? Condition { get; set; }
        public string? DimensionLength { get; set; }
        public string? DimensionWidth { get; set; }
        public string? DimensionUnit { get; set; }
        public string? WinnigBid { get; set; }
        public string? WinnigBidUnit { get; set; }
        public string? EstimateStart { get; set; }
        public string? EstimateEnd { get; set; }
        public string? EstimateUnit { get; set; }
    }
}
