using CurrencyListing.Exports.Abstract;
using CurrencyListing.Models.Base;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace CurrencyListing.Exports.Concrete
{
    public class XmlCurrencyExport : ICurrencyExport
    {
        public FileResult Convert(IEnumerable<CurrencyRate> list)
        {
            var fileResult = new FileResult { Extension = "xml" };
            using var memoryStream = new MemoryStream();
            using (TextWriter streamWriter = new StreamWriter(memoryStream))
            {
                var xmlSerializer = new XmlSerializer(typeof(List<CurrencyRate>));
                xmlSerializer.Serialize(streamWriter, list);
            }

            fileResult.FileData = memoryStream.ToArray();
            return fileResult;
        }
    }
}