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
    public class CurrencyConversionRepository : ICurrencyConversionRepository
    {
        private readonly IDbTransaction _transaction;
        public CurrencyConversionRepository(IDbTransaction transaction)
        {
            _transaction = transaction;
        }

        public async Task<int> AddConversionAsync(CurrencyConversion conversion)
        {
            var sql = "EXEC [dbo].[spAddCurrencyConversion] @FromCurrencyId, @ToCurrencyId, @Rate, @InitialAmount, @ConvertedAmount, @PersonId";
            var parameters = new
            {
                FromCurrencyId = conversion.FromCurrencyId,
                ToCurrencyId = conversion.ToCurrencyId,
                Rate = conversion.Rate,
                InitialAmount = conversion.InitialAmount,
                ConvertedAmount = conversion.ConvertedAmount,
                PersonId = conversion.PersonId
            };

            return await _transaction.Connection.QuerySingleOrDefaultAsync<int>(sql, parameters, _transaction);
        }

        public async Task<IEnumerable<ConversionReport>> GetConversionReportAsync(DateTime? fromDate, DateTime? toDate, string? personalNumber)
        {
            var sql = "EXEC [dbo].[spGetConversionReport] @StartDate, @EndDate, @PersonalNumber";
            var parameters = new
            {
                StartDate = fromDate,
                EndDate = toDate,
                PersonalNumber = personalNumber
            };
            return await _transaction.Connection.QueryAsync<ConversionReport>(sql, parameters, _transaction);
        }
    }
}
