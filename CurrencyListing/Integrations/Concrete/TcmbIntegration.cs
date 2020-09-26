using CurrencyListing.Integrations.Abstract;
using CurrencyListing.Models.Base;
using CurrencyListing.Models.Constants;
using CurrencyListing.Models.Enums;
using System;
using System.Collections.Generic;
using System.Xml;

namespace CurrencyListing.Integrations.Concrete
{
    public class TcmbIntegration : IIntegration
    {
        public XmlDocument Get()
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(SourceConstants.TcmbXmlService);

            return xmlDocument;
        }

        public List<CurrencyRate> Parse(XmlDocument xmlDocument)
        {
            var currencyRates = new List<CurrencyRate>();
            var parentNode = xmlDocument.SelectSingleNode("Tarih_Date");

            var currencyNodes = parentNode.SelectNodes("Currency");
            if (currencyNodes != null && currencyNodes.Count > 0)
            {
                foreach (XmlNode currencyNode in currencyNodes)
                {
                    Currency matchedCurrency;
                    var currencyCode = currencyNode.Attributes["CurrencyCode"].Value;
                    var forexBuying = currencyNode.SelectSingleNode("ForexBuying").InnerText;
                    var forexSelling = currencyNode.SelectSingleNode("ForexSelling").InnerText;
                    var banknoteBuying = currencyNode.SelectSingleNode("BanknoteBuying").InnerText;
                    var banknoteSelling = currencyNode.SelectSingleNode("BanknoteSelling").InnerText;
                    var crossRateUsd = currencyNode.SelectSingleNode("CrossRateUSD").InnerText;
                    var crossRateOther = currencyNode.SelectSingleNode("CrossRateOther").InnerText;

                    if (Enum.TryParse(currencyCode, out matchedCurrency))
                    {
                        var currencyRate = new CurrencyRate();

                        currencyRate.CurrencyCode = matchedCurrency;
                        currencyRate.Name = currencyNode.SelectSingleNode("CurrencyName").InnerText;
                        currencyRate.NameTr = currencyNode.SelectSingleNode("Isim").InnerText;
                        if (!string.IsNullOrWhiteSpace(forexBuying)) currencyRate.ForexBuying = Convert.ToDecimal(forexBuying);
                        if (!string.IsNullOrWhiteSpace(forexSelling)) currencyRate.ForexSelling = Convert.ToDecimal(forexSelling);
                        if (!string.IsNullOrWhiteSpace(banknoteBuying)) currencyRate.BanknoteBuying = Convert.ToDecimal(banknoteBuying);
                        if (!string.IsNullOrWhiteSpace(banknoteSelling)) currencyRate.BanknoteSelling = Convert.ToDecimal(banknoteSelling);
                        if (!string.IsNullOrWhiteSpace(crossRateUsd)) currencyRate.CrossRateUsd = Convert.ToDecimal(crossRateUsd);
                        if (!string.IsNullOrWhiteSpace(crossRateOther)) currencyRate.CrossRateOther = Convert.ToDecimal(crossRateOther);

                        currencyRates.Add(currencyRate);
                    }
                }
            }

            return currencyRates;
        }
    }
}
