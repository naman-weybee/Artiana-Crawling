using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Phillips_Crawling_Task.Service
{
    public class RegexString
    {
        public static readonly Regex TimeDurationRegex = new(@"(\d+)(\s?\,?\-?)(\w+)(\s?\,?\-?)(\d{4})(\s?\,?\-?)(\d+\:\d+\s\w+)(\s?\,?\-?)([^\n\r])(\w+)", RegexOptions.IgnoreCase);
        public static readonly Regex SaleNumberRegex = new(@"(\d+)", RegexOptions.IgnoreCase);
        public static readonly Regex AuctionDateRegex = new(@"(\w+)(\s?\,?\-?)(\d+)(\s?\,?\-?\s?)(\w+)(\s?\,?\-?)(\d+)(\s?\,?\-?\s?)(\d{4})", RegexOptions.IgnoreCase);
        public static readonly Regex DimensionRegex = new(@"(\d+.\d+)(\s?)(\w+)(\s?\w?\s?)(\d+.\d+)(\s?)(\w+)", RegexOptions.IgnoreCase);
    }
}
