using CurrencyListing.Exports.Abstract;
using CurrencyListing.Integrations.Abstract;
using CurrencyListing.Models.Base;
using CurrencyListing.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace CurrencyListing.Helpers
{
    public class CurrencyListingHelper
    {
        private static List<CurrencyRate> _data;

        /// <summary>
        /// Belirtilen entegrasyon tipi ile belirli filtreler ve sıralama özellikleriyle kur listesini döndürür.
        /// </summary>
        /// <param name="integration">Entegrasyon yapılarak listenin döndürüleceği servis türünü belirtir.</param>
        /// <param name="currency">Elde edilmek istenilen kur bilgisini belirtir.</param>
        /// <param name="sortField">Sıralama yapılmak istenilen alanı belirtir.</param>
        /// <param name="sortDirection">Hangi yöne sıralama yapılacağını belirtir. Varsayılan Ascending 'tir.</param>
        /// <returns></returns>
        public BaseResponse<IEnumerable<CurrencyRate>> List(IIntegration integration, Currency? currency = null, SortField? sortField = null, SortDirection? sortDirection = SortDirection.Ascending)
        {
            var response = new BaseResponse<IEnumerable<CurrencyRate>>();
            var xmlDoc = integration.Get();

            if (xmlDoc != null)
            {
                response.DataReceived = true;

                try
                {
                    if (_data == null)
                        _data = integration.Parse(xmlDoc);

                    response.Data = _data;

                    #region Filters

                    if (currency != null)
                    {
                        response.Data = response.Data.Where(c => c.CurrencyCode == (Currency)currency);
                    }

                    if (sortField != null)
                    {
                        var sorting = $"{sortField} {sortDirection.ToString().ToLower()}";
                        response.Data = response.Data.AsQueryable().OrderBy(sorting);
                    }

                    #endregion

                    response.IsDone = true;
                }
                catch (Exception ex)
                {
                    response.IsDone = false;
                    response.ErrorMessage = ex.Message;
                }
            }
            else
            {
                response.DataReceived = false;
                response.ErrorMessage = "Data çekme gerçekleştirilemedi!";
            }

            return response;
        }

        /// <summary>
        /// Belirtilen entegrasyon tipi ile belirli filtreler ve sıralama özellikleriyle kur listesini belirtilen dosya türü ile export eder.
        /// </summary>
        /// <param name="integration">Entegrasyon yapılarak listenin döndürüleceği servis türünü belirtir.</param>
        /// <param name="export">Export edilecek dosya türünü belirtir.</param>
        /// <param name="currency">Elde edilmek istenilen kur bilgisini belirtir.</param>
        /// <param name="sortField">Sıralama yapılmak istenilen alanı belirtir.</param>
        /// <param name="sortDirection">Hangi yöne sıralama yapılacağını belirtir. Varsayılan Ascending 'tir.</param>
        /// <returns></returns>
        public BaseResponse<FileResult> Export(IIntegration integration, ICurrencyExport export, Currency? currency = null, SortField? sortField = null, SortDirection? sortDirection = SortDirection.Ascending)
        {
            var listResponse = List(integration, currency, sortField, sortDirection);

            var response = new BaseResponse<FileResult>
            {
                IsDone = true,
                DataReceived = listResponse.DataReceived,
                ErrorMessage = listResponse.ErrorMessage
            };

            if (listResponse.IsDone &&
                listResponse.Data != null &&
                listResponse.Data.Any())
            {
                try
                {
                    response.Data = export.Convert(listResponse.Data);
                }
                catch (Exception ex)
                {
                    response.IsDone = false;
                    response.ErrorMessage = ex.Message;
                }
            }

            return response;
        }
    }
}
