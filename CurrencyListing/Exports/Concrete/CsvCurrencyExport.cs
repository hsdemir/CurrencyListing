using CsvHelper;
using CurrencyListing.Exports.Abstract;
using CurrencyListing.Models.Base;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace CurrencyListing.Exports.Concrete
{
    public class CsvCurrencyExport : ICurrencyExport
    {
        public FileResult Convert(IEnumerable<CurrencyRate> list)
        {
            var fileResult = new FileResult { Extension = "csv" };
            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream))
            using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
            {
                csvWriter.WriteRecords(list);
            }

            fileResult.FileData = memoryStream.ToArray();
            return fileResult;
        }
    }
}