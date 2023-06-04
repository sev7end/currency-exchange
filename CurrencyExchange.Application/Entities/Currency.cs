using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Application.Entities
{
    public class Currency
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string NameKa { get; set; }
    }
}
