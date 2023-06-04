using CurrencyExchange.Application.Entities;
using CurrencyExchange.Application.Interfaces.Repositories;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Infrastructure.Implementations.Repositories
{
    public class CurrencyRateRepository : ICurrencyRateRepository
    {
        private readonly IDbTransaction _transaction;
        public CurrencyRateRepository(IDbTransaction transaction)
        {
            _transaction = transaction;
        }

        public async Task<int> AddAsync(CurrencyRate rate)
        {
            var sql = "EXEC [dbo].[spAddCurrencyRate] @CurrencyId,@BuyRate,@SellRate";
            var parameters = new { CurrencyId = rate.CurrencyId, BuyRate = rate.BuyRate, SellRate = rate.SellRate };
            return await _transaction.Connection.QuerySingleOrDefaultAsync<int>(sql, parameters,_transaction);
        }

        public async Task<CurrencyRate> GetLatestCurrencyRateAsync(int currencyId)
        {
            var sql = "EXEC [dbo].[spGetLatestCurrencyRate] @CurrencyId";
            var parameters = new { CurrencyId = currencyId };
            return await _transaction.Connection.QuerySingleOrDefaultAsync<CurrencyRate>(sql, parameters,_transaction);  
        }
    }
}
