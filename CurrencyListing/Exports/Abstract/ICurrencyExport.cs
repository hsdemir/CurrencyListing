using CurrencyListing.Models.Base;
using System.Collections.Generic;
using System.IO;

namespace CurrencyListing.Exports.Abstract
{
    public interface ICurrencyExport
    {
        FileResult Convert(IEnumerable<CurrencyRate> list);
    }
}
