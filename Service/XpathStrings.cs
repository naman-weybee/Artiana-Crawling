using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phillips_Crawling_Task.Service
{
    public class XpathStrings
    {
        public static readonly string PastAuctionsXpath = "//a[.='Past Auctions']";

        public static readonly string FineWatchAuctionsXpath = "//*[contains(text(),'Fine Watches')]/ancestor::div[@class='pastBox']";

        public static readonly string WatchAuctionImageUrlXpath = ".//div[@class='auctionImg']/img";
        public static readonly string AuctionTitleXpath = ".//h4[@class='auctionName']";
        public static readonly string SaleNumberXpath = ".//h5[contains(text(),'Sale Number')]";
        public static readonly string TimeDurationXpath = ".//p[@class='auctionDate']";
        public static readonly string AuctionResultLinkXpath = "//*[contains(text(),'Fine Watches')]/ancestor::div[@class='pastBox']/div/div[@class='auctionBtn']/a[.='View Auction Result']";

        public static readonly string AuctionDateXpath = ".//p[@class='auctionDate']";
        public static readonly string WatchListXpath = "//div[@class='lotBox lightbox d-lg-flex justify-content-lg-between']";
        public static readonly string WatchImageUrlXpath = ".//img[@class='shadow-img1 mb-4']";
        public static readonly string WatchArtistXpath = ".//h4[@class='artistName']";
        public static readonly string WatchPaintingNameXpath = ".//p[@class='paintingName']";
        public static readonly string WatchInfoXpath = ".//p[@class='info']";
    }
}
