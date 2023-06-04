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
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly IDbTransaction _transaction;

        public CurrencyRepository(IDbTransaction transaction)
        {
            _transaction = transaction;
        }

        public async Task<int> AddAsync(Currency currency)
        {
            var sql = "EXEC dbo.spAddCurrency @Code, @NameKa";
            var parameters = new { Code = currency.Code, NameKa = currency.NameKa };

            return await _transaction.Connection.QuerySingleOrDefaultAsync<int>(sql, parameters, _transaction);
        }

        public async Task<IEnumerable<CurrencyInfo>> GetAllAsync()
        {
            var sql = "EXEC dbo.spGetAllCurrencies";
            return await _transaction.Connection.QueryAsync<CurrencyInfo>(sql,null,_transaction);
        }
    }
}
