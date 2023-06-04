using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Application.DataTransferObjects.Responses
{
    public class CurrencyInfoResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal BuyRate { get; set; }
        public decimal SellRate { get; set; }
        public DateTime RateDate { get; set; }
    }
}
