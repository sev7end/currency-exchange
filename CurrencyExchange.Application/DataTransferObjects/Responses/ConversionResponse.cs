using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Application.DataTransferObjects.Responses
{
    public class ConversionResponse
    {
        public string FromExchangeRate { get; set; }
        public string ToExchangeRate { get; set;}
        public decimal ExchangeRate { get; set; }
        public DateTime ExchangeDate { get; set; }
        public decimal InitialAmount { get; set; }
        public decimal ConvertedAmount { get; set; }
        public string FullName { get; set; }
        public string PersonalNumber { get; set; }
    }
}
