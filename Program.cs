using Artiana_Crawling.Data;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using Phillips_Crawling_Task.Service;
using System.Xml.Linq;

namespace Artiana_Crawling
{
    class Program
    {
        private const string Url = "https://artiana.com/PastAuctions.aspx";
        private const string BaseUrl = "https://artiana.com/";
        private static readonly ArtianaWatchesDbContext _context = new();
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

        private static async void GetWatchDetails()
        {
            ChromeOptions opt = new();
            opt.AddArgument("--log-level=3");
            opt.AddArguments("--disable-gpu");
            opt.AddArguments("--start-maximized");
            opt.AddArguments("--no-sandbox");

            ChromeDriver driver = new(ChromeDriverService.CreateDefaultService(), opt, TimeSpan.FromMinutes(3));
            driver.Navigate().GoToUrl(Url);
            var pageSource = GetFullyLoadedWebPageContent(driver);
            var Details = new HtmlDocument();
            Details.LoadHtml(pageSource);

            var allFineWatchAuctions = Details.DocumentNode.SelectNodes(XpathStrings.FineWatchAuctionsXpath);
            try
            {
                if (allFineWatchAuctions != null)
                {
                    foreach (var watchAuction in allFineWatchAuctions)
                    {
                        var auctionImageUrl = string.Empty;
                        var auctionTitle = string.Empty;
                        var saleNumberString = string.Empty;
                        var saleNumber = string.Empty;
                        var timeDurationString = string.Empty;
                        var auctionDateString = string.Empty;
                        var auctionResultLink = string.Empty;
                        var startDate = string.Empty;
                        var endDate = string.Empty;
                        var startMonth = string.Empty;
                        var startYear = string.Empty;
                        var endMonth = string.Empty;
                        var endYear = string.Empty;
                        var endTime = string.Empty;
                        var countryTimeZone = string.Empty;
                        var auctionStatus = string.Empty;

                        auctionImageUrl = BaseUrl + watchAuction.SelectSingleNode(XpathStrings.WatchAuctionImageUrlXpath)?.GetAttributes("src").First().Value ?? string.Empty;
                        auctionTitle = watchAuction.SelectSingleNode(XpathStrings.AuctionTitleXpath)?.InnerText.Trim() ?? string.Empty;
                        saleNumberString = watchAuction.SelectSingleNode(XpathStrings.SaleNumberXpath).InnerText?.Trim() ?? string.Empty;
                        timeDurationString = watchAuction.SelectSingleNode(XpathStrings.TimeDurationXpath)?.InnerText.Trim() ?? string.Empty;

                        IWebElement element = driver.FindElement(By.XPath(XpathStrings.AuctionResultLinkXpath + $"[{allFineWatchAuctions.IndexOf(watchAuction) + 1}]"));
                        Actions action = new(driver);
                        action.MoveToElement(element).Click().Perform();

                        string watchPageSource = GetFullyLoadedWebPageContent(driver);
                        var watchDetails = new HtmlDocument();
                        watchDetails.LoadHtml(watchPageSource);
                        auctionResultLink = driver.Url ?? string.Empty;
                        var auctionId = RegexString.AuctionIdRegex.Match(auctionResultLink).Groups[1].Value;
                        auctionDateString = watchDetails.DocumentNode.SelectSingleNode(XpathStrings.AuctionDateXpath)?.InnerText.Trim() ?? string.Empty;
                        auctionStatus = watchDetails.DocumentNode.SelectSingleNode(XpathStrings.AuctionStatusXpath)?.InnerText.Trim() ?? string.Empty;

                        if (!string.IsNullOrEmpty(saleNumberString))
                            saleNumber = RegexString.SaleNumberRegex.Match(saleNumberString)?.Groups[1].Value ?? string.Empty;

                        if (!string.IsNullOrEmpty(auctionDateString))
                        {
                            startDate = RegexString.AuctionDateRegex.Match(auctionDateString)?.Groups[3].Value ?? string.Empty;
                            startMonth = RegexString.AuctionDateRegex.Match(auctionDateString)?.Groups[1].Value ?? string.Empty;
                            startYear = RegexString.AuctionDateRegex.Match(auctionDateString)?.Groups[9].Value ?? string.Empty;
                            endDate = RegexString.AuctionDateRegex.Match(auctionDateString)?.Groups[7].Value ?? string.Empty;
                            endMonth = RegexString.AuctionDateRegex.Match(auctionDateString)?.Groups[5].Value ?? string.Empty;
                            endYear = RegexString.AuctionDateRegex.Match(auctionDateString)?.Groups[9].Value ?? string.Empty;
                        }

                        if (!string.IsNullOrEmpty(timeDurationString))
                        {
                            endTime = RegexString.TimeDurationRegex.Match(timeDurationString)?.Groups[7].Value ?? string.Empty;
                            countryTimeZone = RegexString.TimeDurationRegex.Match(timeDurationString)?.Groups[10].Value ?? string.Empty;
                        }

                        Console.WriteLine($"----------Auction with Id = {auctionId}----------");
                        Console.WriteLine($"Auction Title: {auctionTitle}");
                        Console.WriteLine($"Auction Image Url: {auctionImageUrl}");
                        Console.WriteLine($"Auction Result Link: {auctionResultLink}");
                        Console.WriteLine($"Sale Number {saleNumber}");
                        Console.WriteLine($"Start Date: {startDate}");
                        Console.WriteLine($"Start Month: {startMonth}");
                        Console.WriteLine($"Start Year: {startYear}");
                        Console.WriteLine($"End Date: {endDate}");
                        Console.WriteLine($"End Month: {endMonth}");
                        Console.WriteLine($"End Year: {endYear}");
                        Console.WriteLine($"End Time: {endTime}");
                        Console.WriteLine($"Country Time Zone: {countryTimeZone}");
                        Console.WriteLine($"Auction Status: {auctionStatus}");
                        Console.WriteLine();

                        var auctionRecord = await _context.tbl_Watch_Auctions.Where(x => x.Id == auctionId.ToString()).FirstOrDefaultAsync();
                        if (auctionRecord != null)
                        {
                            auctionRecord.Id = auctionId.ToString();
                            auctionRecord.Title = auctionTitle;
                            auctionRecord.ImageUrl = auctionTitle;
                            auctionRecord.Link = auctionResultLink;
                            auctionRecord.SaleNumber = saleNumber;
                            auctionRecord.StartDate = startDate;
                            auctionRecord.StartMonth = startMonth;
                            auctionRecord.StartYear = startYear;
                            auctionRecord.EndDate = endDate;
                            auctionRecord.EndMonth = endMonth;
                            auctionRecord.EndYear = endYear;
                            auctionRecord.EndTime = endTime;
                            auctionRecord.CountryTimeZone = countryTimeZone;
                            auctionRecord.AuctionStatus = auctionStatus;
                            _context.tbl_Watch_Auctions.Update(auctionRecord);
                        }
                        else
                        {
                            WatchAuctions watchAuctions = new()
                            {
                                Id = auctionId.ToString(),
                                Title = auctionTitle,
                                ImageUrl = auctionImageUrl,
                                Link = auctionResultLink,
                                SaleNumber = saleNumber,
                                StartDate = startDate,
                                StartMonth = startMonth,
                                StartYear = startYear,
                                EndDate = endDate,
                                EndMonth = endMonth,
                                EndYear = endYear,
                                EndTime = endTime,
                                CountryTimeZone = countryTimeZone,
                                AuctionStatus = auctionStatus
                            };
                            await _context.tbl_Watch_Auctions.AddAsync(watchAuctions);
                        }

                        var watchDetailsList = watchDetails.DocumentNode.SelectNodes(XpathStrings.WatchListXpath);
                        if (watchDetailsList != null)
                        {
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

                                watchImageUrl = BaseUrl + watch.SelectSingleNode(XpathStrings.WatchImageUrlXpath)?.GetAttributes("src").First().Value ?? string.Empty;
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

                                var watchRecord = await _context.tbl_Watch_Details.Where(x => x.AuctionId == auctionId.ToString()).FirstOrDefaultAsync();
                                if (watchRecord != null)
                                {
                                    watchRecord.Id = watchRecord.Id;
                                    watchRecord.AuctionId = auctionId.ToString();
                                    watchRecord.ImageUrl = watchImageUrl;
                                    watchRecord.WatchArtist = watchArtist;
                                    watchRecord.WatchPaintingName = watchPaintingName;
                                    watchRecord.RefrenceNo = refrenceNo;
                                    watchRecord.Circa = circa;
                                    watchRecord.CaseMaterial = caseMaterial;
                                    watchRecord.Condition = condition;
                                    watchRecord.DimensionLength = dimensionLength;
                                    watchRecord.DimensionWidth = dimensionWidth;
                                    watchRecord.DimensionUnit = dimensionUnit;
                                    watchRecord.WinnigBid = winnigBid;
                                    watchRecord.WinnigBidUnit = winnigBidUnit;
                                    watchRecord.EstimateStart = estimateStart;
                                    watchRecord.EstimateEnd = estimateEnd;
                                    watchRecord.EstimateUnit = estimateUnit;
                                    _context.tbl_Watch_Details.Update(watchRecord);
                                }
                                else
                                {
                                    WatchDetails watchDetails1 = new()
                                    {
                                        AuctionId = auctionId.ToString(),
                                        ImageUrl = watchImageUrl,
                                        WatchArtist = watchArtist,
                                        WatchPaintingName = watchPaintingName,
                                        RefrenceNo = refrenceNo,
                                        Circa = circa,
                                        CaseMaterial = caseMaterial,
                                        Condition = condition,
                                        DimensionLength = dimensionLength,
                                        DimensionWidth = dimensionWidth,
                                        DimensionUnit = dimensionUnit,
                                        WinnigBid = winnigBid,
                                        WinnigBidUnit = winnigBidUnit,
                                        EstimateStart = estimateStart,
                                        EstimateEnd = estimateEnd,
                                        EstimateUnit = estimateUnit,
                                    };
                                    await _context.tbl_Watch_Details.AddAsync(watchDetails1);
                                }
                            }
                        }
                        await _context.SaveChangesAsync();
                        Console.WriteLine();
                        Console.WriteLine("=====================================================================================================================");
                        Console.WriteLine($"Data Saved in WatchAuctions and WatchDetails Tables with AuctionId = {auctionId}");
                        Console.WriteLine("=====================================================================================================================");
                        Console.WriteLine();
                        driver.Navigate().Back();
                        GetFullyLoadedWebPage(driver);
                    }
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