using CurrencyExchange.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Application.Interfaces.Repositories
{
    public interface ICurrencyRepository
    {
        Task<int> AddAsync(Currency currency);
        Task<IEnumerable<CurrencyInfo>> GetAllAsync();
    }
}
