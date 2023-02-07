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
                    var saleNumberString = string.Empty;
                    var saleNumber = string.Empty;
                    var timeDurationString = string.Empty;
                    var auctionResultLink = string.Empty;
                    var endDate = string.Empty;
                    var endMonth = string.Empty;
                    var endYear = string.Empty;
                    var endTime = string.Empty;
                    var countryTimeZone = string.Empty;

                    auctionImageUrl = Url + watchAuction.SelectSingleNode(XpathStrings.WatchAuctionImageUrlXpath)?.GetAttributes("src").First().Value ?? string.Empty;
                    auctionTitle = watchAuction.SelectSingleNode(XpathStrings.AuctionTitleXpath)?.InnerHtml.Trim() ?? string.Empty;
                    saleNumberString = watchAuction.SelectSingleNode(XpathStrings.SaleNumberXpath).InnerHtml?.Trim() ?? string.Empty;
                    timeDurationString = watchAuction.SelectSingleNode(XpathStrings.TimeDurationXpath)?.InnerHtml.Trim() ?? string.Empty;

                    IWebElement element = driver.FindElement(By.XPath(XpathStrings.AuctionResultLinkXpath + $"[{allFineWatchAuctions.IndexOf(watchAuction) + 1}]"));
                    Actions action = new(driver);
                    action.MoveToElement(element).Click().Perform();

                    string watchPageSource = GetFullyLoadedWebPageContent(driver);
                    var watchDetails = new HtmlDocument();
                    watchDetails.LoadHtml(watchPageSource);
                    auctionResultLink = driver.Url ?? string.Empty;
                    var auctionId = RegexString.AuctionIdRegex.Match(auctionResultLink);

                    if (!string.IsNullOrEmpty(saleNumberString))
                        saleNumber = RegexString.SaleNumberRegex.Match(saleNumberString)?.Groups[1].Value ?? string.Empty;

                    if (!string.IsNullOrEmpty(timeDurationString))
                    {
                        endDate = RegexString.AuctionDateRegex.Match(saleNumberString)?.Groups[1].Value ?? string.Empty;
                        endMonth = RegexString.AuctionDateRegex.Match(saleNumberString)?.Groups[3].Value ?? string.Empty;
                        endYear = RegexString.AuctionDateRegex.Match(saleNumberString)?.Groups[5].Value ?? string.Empty;
                        endTime = RegexString.AuctionDateRegex.Match(saleNumberString)?.Groups[7].Value ?? string.Empty;
                        countryTimeZone = RegexString.AuctionDateRegex.Match(saleNumberString)?.Groups[10].Value ?? string.Empty;
                    }

                    Console.WriteLine($"----------Auction with Id = {auctionId}----------");
                    Console.WriteLine($"Auction Title: {auctionTitle}");
                    Console.WriteLine($"Auction Image Url: {auctionImageUrl}");
                    Console.WriteLine($"Auction Result Link: {auctionResultLink}");
                    Console.WriteLine($"Sale Number {saleNumber}");
                    Console.WriteLine($"Auction Date: {endDate}");
                    Console.WriteLine($"Auction Date: {endMonth}");
                    Console.WriteLine($"Auction Date: {endYear}");
                    Console.WriteLine($"Auction Date: {endTime}");
                    Console.WriteLine($"Auction Date: {countryTimeZone}");
                    Console.WriteLine();

                    var watchDetailsList = watchDetails.DocumentNode.SelectNodes(XpathStrings.WatchListXpath);
                    foreach (var watch in watchDetailsList)
                    {
                        var watchImageUrl = string.Empty;
                        var watchArtist = string.Empty;
                        var watchPaintingName = string.Empty;
                        var watchInfoString = string.Empty;
                        var refrenceNo = string.Empty;
                        var circa = string.Empty;
                        var caseMaterial = string.Empty;
                        var condition = string.Empty;
                        var caseDiameter = string.Empty;
                        var dimensionLength = string.Empty;
                        var dimensionWidth = string.Empty;
                        var dimensionUnit = string.Empty;
                        var winnigBidString = string.Empty;
                        var winnigBidUnit = string.Empty;
                        var winnigBid = string.Empty;
                        var estimateString = string.Empty;
                        var estimateStart = string.Empty;
                        var estimateEnd = string.Empty;
                        var estimateUnit = string.Empty;

                        watchImageUrl = Url + watch.SelectSingleNode(XpathStrings.WatchImageUrlXpath)?.GetAttributes("src").First().Value ?? string.Empty;
                        watchArtist = watch.SelectSingleNode(XpathStrings.WatchArtistXpath)?.InnerText.Trim() ?? string.Empty;
                        watchPaintingName = watch.SelectSingleNode(XpathStrings.WatchPaintingNameXpath)?.InnerText.Trim() ?? string.Empty;
                        watchInfoString = watch.SelectSingleNode(XpathStrings.WatchInfoXpath)?.InnerHtml.Trim() ?? string.Empty;
                        winnigBidString = watch.SelectSingleNode(XpathStrings.WinningBidXpath)?.InnerText.Replace(",", "").Trim() ?? string.Empty;
                        estimateString = watch.SelectSingleNode(XpathStrings.EstimateXpath)?.InnerText.Replace(",", "").Trim() ?? string.Empty;

                        if (!string.IsNullOrEmpty(watchInfoString))
                        {
                            refrenceNo = RegexString.ReferenceNoRegex.Match(watchInfoString)?.Groups[1].Value ?? string.Empty;
                            circa = RegexString.CircaRegex.Match(watchInfoString)?.Groups[1].Value ?? string.Empty;
                            caseMaterial = RegexString.CaseMaterialRegex.Match(watchInfoString)?.Groups[1].Value ?? string.Empty;
                            condition = RegexString.ConditionRegex.Match(watchInfoString)?.Groups[1].Value ?? string.Empty;
                            caseDiameter = RegexString.CaseDiameterRegex.Match(watchInfoString)?.Groups[1].Value ?? string.Empty;
                            if (!string.IsNullOrEmpty(caseDiameter))
                            {
                                dimensionLength = RegexString.CaseDimensionRegex.Match(caseDiameter)?.Groups[1].Value ?? string.Empty;
                                dimensionWidth = RegexString.CaseDimensionRegex.Match(caseDiameter)?.Groups[5].Value ?? string.Empty;
                                dimensionUnit = RegexString.CaseDimensionRegex.Match(caseDiameter)?.Groups[3].Value ?? string.Empty;
                            }
                        }

                        if (!string.IsNullOrEmpty(winnigBidString))
                        {
                            winnigBid = RegexString.WinningBidRegex.Match(winnigBidString)?.Groups[7].Value ?? string.Empty;
                            winnigBidUnit = RegexString.WinningBidRegex.Match(winnigBidString)?.Groups[5].Value ?? string.Empty;
                        }

                        if (!string.IsNullOrEmpty(estimateString))
                        {
                            estimateStart = RegexString.EstimateRegex.Match(estimateString)?.Groups[5].Value ?? string.Empty;
                            estimateEnd = RegexString.EstimateRegex.Match(estimateString)?.Groups[7].Value ?? string.Empty;
                            estimateUnit = RegexString.EstimateRegex.Match(estimateString)?.Groups[3].Value ?? string.Empty;
                        }

                        Console.WriteLine($"----------Watch with Auction Id = {auctionId}----------");
                        Console.WriteLine($"Watch Image Url: {watchImageUrl}");
                        Console.WriteLine($"Watch Artist: {watchArtist}");
                        Console.WriteLine($"Watch Painting Name: {watchPaintingName}");
                        Console.WriteLine($"Refrence No: {refrenceNo}");
                        Console.WriteLine($"Circa: {circa}");
                        Console.WriteLine($"Case Material: {caseMaterial}");
                        Console.WriteLine($"Condition: {condition}");
                        Console.WriteLine($"Dimension Length: {dimensionLength}");
                        Console.WriteLine($"Dimension Width: {dimensionWidth}");
                        Console.WriteLine($"Dimension Unit: {dimensionUnit}");
                        Console.WriteLine($"Winnig Bid: {winnigBid}");
                        Console.WriteLine($"Winnig Bid Unit: {winnigBidUnit}");
                        Console.WriteLine($"Estimate Start: {estimateStart}");
                        Console.WriteLine($"Estimate End: {estimateEnd}");
                        Console.WriteLine($"Estimate Unit: {estimateUnit}");
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                    Console.WriteLine("=====================================================================================================================");
                    Console.WriteLine($"Data Saved in WatchAuctions and WatchDetails Tables with Auction Id = {auctionId}");
                    Console.WriteLine("=====================================================================================================================");
                    Console.WriteLine();
                    driver.Navigate().Back();
                    GetFullyLoadedWebPage(driver);
                }
                driver.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}