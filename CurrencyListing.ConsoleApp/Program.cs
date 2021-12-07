using CurrencyListing.ConsoleApp.Exports;
using CurrencyListing.Helpers;
using CurrencyListing.Integrations.Concrete;
using CurrencyListing.Models.Enums;
using System;
using System.IO;
using System.Linq;

namespace CurrencyListing.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var currencyListingHelper = new CurrencyListingHelper();
            List(currencyListingHelper);
            Export(currencyListingHelper);     
        }

        private static void List(CurrencyListingHelper currencyListingHelper)
        {
            var listResponse = currencyListingHelper.List(new TcmbIntegration(), sortField: SortField.ForexBuying, sortDirection: SortDirection.Descending);

            if (listResponse.IsDone &&
                listResponse.Data != null &&
                listResponse.Data.Any())
            {
                Console.WriteLine("Currency | Forex buying | Forex Selling | Banknote buying | Banknote Selling");
                foreach (var currency in listResponse.Data)
                {
                    Console.WriteLine($"{currency.NameTr} ({currency.CurrencyCode}) | {currency.ForexBuying} | {currency.ForexSelling} | {currency.BanknoteBuying} | {currency.BanknoteSelling}");
                }
            }

            Console.ReadLine();
        }

        private static void Export(CurrencyListingHelper currencyListingHelper)
        {
            var exportResponse = currencyListingHelper.Export(new TcmbIntegration(), new JsonExport());

            if (exportResponse.IsDone &&
                exportResponse.Data != null &&
                exportResponse.Data.FileData != null)
            {
                var fileResult = exportResponse.Data;
                var rootFolder = "C:\\exports";
                var subFolder = $"{rootFolder}\\{exportResponse.Data.Extension}";
                if (!Directory.Exists(rootFolder)) Directory.CreateDirectory(rootFolder);
                if (!Directory.Exists(subFolder)) Directory.CreateDirectory(subFolder);

                File.WriteAllBytes($"C:\\exports\\{fileResult.Extension}\\data.{fileResult.Extension}", fileResult.FileData);

            }
        }
    }
}
