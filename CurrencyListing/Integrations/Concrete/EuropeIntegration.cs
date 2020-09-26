using CurrencyListing.Integrations.Abstract;
using CurrencyListing.Models.Base;
using CurrencyListing.Models.Constants;
using System;
using System.Collections.Generic;
using System.Xml;

namespace CurrencyListing.Integrations.Concrete
{
    public class EuropeIntegration : IIntegration
    {
        public XmlDocument Get()
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(SourceConstants.EuropeXmlService);

            return xmlDocument;
        }

        public List<CurrencyRate> Parse(XmlDocument xmlDocument)
        {
            throw new NotImplementedException();
        }
    }
}
