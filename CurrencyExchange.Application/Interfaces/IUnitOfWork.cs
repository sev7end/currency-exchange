using CurrencyExchange.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICurrencyRepository Currencies { get; }
        IPersonRepository Persons { get; }
        ICurrencyConversionRepository CurrencyConversions { get; }
        ICurrencyRateRepository CurrencyRates { get; }

        void Complete();
    }
}
