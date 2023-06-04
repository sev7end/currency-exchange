using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Application.Entities
{
    public class CurrencyRate
    {
        public int Id { get; set; }
        public int CurrencyId { get; set; }
        public decimal BuyRate { get; set; }
        public decimal SellRate { get; set; }
        public DateTime RateDate { get; set; }

    }
}
