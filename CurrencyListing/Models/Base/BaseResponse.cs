using System.Collections.Generic;

namespace CurrencyListing.Models.Base
{
    public class BaseResponse<T>
    {
        public bool IsDone { get; set; }
        public bool DataReceived { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }
    }
}
