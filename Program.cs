using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using Phillips_Crawling_Task.Service;
using System.Xml.Linq;

namespace Artiana_Crawling
{
    class Program
    {
        private const string Url = "https://artiana.com/";
        static void Main(string[] args)
        {
            GetWatchDetails();
            Console.ReadLine();
        }

        public static void GetFullyLoadedWebPage(WebDriver driver)
        {
            long scrollHeight = 0;
            IJavaScriptExecutor js = driver;
            do
            {
                var newScrollHeight = (long)js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight); return document.body.scrollHeight;");
                if (newScrollHeight != scrollHeight)
                {
                    scrollHeight = newScrollHeight;
                    Thread.Sleep(3000);
                }
                else
                {
                    Thread.Sleep(4000);
                    break;
                }
            } while (true);
        }

        public static string GetFullyLoadedWebPageContent(WebDriver driver)
        {
            long scrollHeight = 0;
            IJavaScriptExecutor js = driver;
            do
            {
                var newScrollHeight = (long)js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight); return document.body.scrollHeight;");
                if (newScrollHeight != scrollHeight)
                {
                    scrollHeight = newScrollHeight;
                    Thread.Sleep(3000);
                }
                else
                {
                    Thread.Sleep(4000);
                    break;
                }
            } while (true);
            return driver.PageSource;
        }

        private static void GetWatchDetails()
        {
            ChromeOptions opt = new();
            opt.AddArgument("--log-level=3");
            opt.AddArguments("--disable-gpu");
            opt.AddArguments("--start-maximized");
            opt.AddArguments("--no-sandbox");

            ChromeDriver driver = new(ChromeDriverService.CreateDefaultService(), opt, TimeSpan.FromMinutes(3));
            driver.Navigate().GoToUrl(Url);
            GetFullyLoadedWebPage(driver);

            driver.FindElement(By.XPath(XpathStrings.PastAuctionsXpath)).Click();
            var pageSource = GetFullyLoadedWebPageContent(driver);
            var Details = new HtmlDocument();
            Details.LoadHtml(pageSource);

            var allFineWatchAuctions = Details.DocumentNode.SelectNodes(XpathStrings.FineWatchAuctionsXpath);
            try
            {
                foreach (var watchAuction in allFineWatchAuctions)
                {
                    var auctionImageUrl = string.Empty;
                    var auctionTitle = string.Empty;
                    var saleNumber = string.Empty;
                    var auctionDate = string.Empty;
                    var auctionResultLink = string.Empty;

                    auctionImageUrl = Url + watchAuction.SelectSingleNode(XpathStrings.WatchAuctionImageUrlXpath)?.GetAttributes("src").First().Value ?? string.Empty;
                    auctionTitle = watchAuction.SelectSingleNode(XpathStrings.AuctionTitleXpath)?.InnerHtml.Trim() ?? string.Empty;
                    saleNumber = watchAuction.SelectSingleNode(XpathStrings.SaleNumberXpath).InnerHtml?.Trim() ?? string.Empty;
                    auctionDate = watchAuction.SelectSingleNode(XpathStrings.TimeDurationXpath)?.InnerHtml.Trim() ?? string.Empty;

                    driver.FindElement(By.XPath(XpathStrings.AuctionResultLinkXpath)).Click();
                    string watchPageSource = GetFullyLoadedWebPageContent(driver);
                    var watchDetails = new HtmlDocument();
                    watchDetails.LoadHtml(watchPageSource);

                    var watchDetailsList = watchDetails.DocumentNode.SelectNodes(XpathStrings.WatchListXpath);
                    foreach (var watch in watchDetailsList)
                    {
                        var watchImageUrl = string.Empty;
                        var watchArtist = string.Empty;
                        var watchPaintingName = string.Empty;
                        var watchInfo = string.Empty;

                        watchImageUrl = Url + watch.SelectSingleNode(XpathStrings.WatchImageUrlXpath)?.GetAttributes("src").First().Value ?? string.Empty;
                        watchArtist = watch.SelectSingleNode(XpathStrings.WatchArtistXpath)?.InnerHtml.Trim() ?? string.Empty;
                        watchPaintingName = watch.SelectSingleNode(XpathStrings.WatchPaintingNameXpath)?.InnerHtml.Trim() ?? string.Empty;
                        watchInfo = watch.SelectSingleNode(XpathStrings.WatchInfoXpath)?.InnerHtml.Trim() ?? string.Empty;

                        Console.WriteLine("========");
                        Console.WriteLine(watchInfo);
                        Console.WriteLine("========");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}