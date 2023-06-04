using CurrencyExchange.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Application.Interfaces.Repositories
{
    public interface ICurrencyRateRepository
    {
        Task<int> AddAsync(CurrencyRate rate);
        Task<CurrencyRate> GetLatestCurrencyRateAsync(int currencyId);
    }
}
