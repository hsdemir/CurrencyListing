using CurrencyListing.Exports.Concrete;
using CurrencyListing.Helpers;
using CurrencyListing.Integrations.Concrete;
using CurrencyListing.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CurrencyListing.Test
{
    public class CurrencyHelperTest
    {
        [Fact]
        public void List_CurrencyRateList_HaveData()
        {
            // Arrange
            var integration = new TcmbIntegration();
            var currencyHelper = new CurrencyListingHelper();

            // Act
            var result = currencyHelper.List(integration, Currency.USD);

            // Assert
            Assert.NotNull(result.Data);
        }

        [Fact]
        public void List_CurrencyRateList_CorrectFilter()
        {
            // Arrange
            var integration = new TcmbIntegration();
            var currencyHelper = new CurrencyListingHelper();

            // Act
            var result = currencyHelper.List(integration, Currency.USD);

            // Assert
            Assert.Equal(Currency.USD, result.Data.FirstOrDefault().CurrencyCode);
        }

        [Fact]
        public void List_CurrencyRateList_CorrectSorter()
        {
            // Arrange
            var integration = new TcmbIntegration();
            var currencyHelper = new CurrencyListingHelper();

            // Act
            var response = currencyHelper.List(integration, sortField: SortField.ForexBuying, sortDirection: SortDirection.Descending);
            var result = response.Data.First().ForexBuying > response.Data.Last().ForexBuying;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Export_CurrencyRateListExport_HaveFileResult()
        {
            // Arrange
            var integration = new TcmbIntegration();
            var currencyExport = new CsvCurrencyExport();
            var currencyHelper = new CurrencyListingHelper();

            // Act
            var response = currencyHelper.Export(integration, currencyExport);

            // Assert
            Assert.NotNull(response.Data.FileData);
        }

        [Fact]
        public void Export_CurrencyRateListExport_HaveCsvFileResult()
        {
            // Arrange
            var integration = new TcmbIntegration();
            var currencyExport = new CsvCurrencyExport();
            var currencyHelper = new CurrencyListingHelper();

            // Act
            var response = currencyHelper.Export(integration, currencyExport);

            // Assert
            Assert.Equal("csv", response.Data.Extension);
        }

        [Fact]
        public void Export_CurrencyRateListExport_HaveXmlFileResult()
        {
            // Arrange
            var integration = new TcmbIntegration();
            var currencyExport = new XmlCurrencyExport();
            var currencyHelper = new CurrencyListingHelper();

            // Act
            var response = currencyHelper.Export(integration, currencyExport);

            // Assert
            Assert.Equal("xml", response.Data.Extension);
        }
    }
}
