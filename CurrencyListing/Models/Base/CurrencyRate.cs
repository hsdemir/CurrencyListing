using CurrencyListing.Models.Enums;
using System;

namespace CurrencyListing.Models.Base
{
    public class CurrencyRate
    {
        public Currency CurrencyCode { get; set; }
        public int Unit { get; set; }
        public string Name { get; set; }
        public string NameTr { get; set; }
        public decimal? ForexBuying { get; set; }
        public decimal? ForexSelling { get; set; }
        public decimal? BanknoteBuying { get; set; }
        public decimal? BanknoteSelling { get; set; }
        public decimal? CrossRateUsd { get; set; }
        public decimal? CrossRateOther { get; set; }
    }
}
