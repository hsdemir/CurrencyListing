using CurrencyListing.Exports.Abstract;
using CurrencyListing.Models.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace CurrencyListing.ConsoleApp.Exports
{
    public class JsonExport : ICurrencyExport
    {
        public FileResult Convert(IEnumerable<CurrencyRate> list)
        {
            var fileResult = new FileResult { Extension = "json" };
            using var memoryStream = new MemoryStream();
            using (TextWriter streamWriter = new StreamWriter(memoryStream))
            {
                streamWriter.Write(JsonConvert.SerializeObject(list));
            }

            fileResult.FileData = memoryStream.ToArray();
            return fileResult;
        }
    }
}
