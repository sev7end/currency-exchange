using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Application.Entities
{
    public class CurrencyConversion
    {
        public int Id { get; set; }
        public int FromCurrencyId { get; set; }
        public int ToCurrencyId { get; set;}
        public decimal Rate { get; set; }
        public decimal InitialAmount { get; set; }
        public decimal ConvertedAmount { get; set; }
        public int PersonId { get; set; }
        public DateTime CreateDate {  get; set; }
    }
}
