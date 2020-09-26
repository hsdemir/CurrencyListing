using CurrencyListing.Models.Base;
using System.Collections.Generic;
using System.Xml;

namespace CurrencyListing.Integrations.Abstract
{
    public interface IIntegration
    {
        XmlDocument Get();
        List<CurrencyRate> Parse(XmlDocument xmlDocument);
    }
}
