using CurrencyExchange.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Application.Interfaces.Repositories
{
    public interface ICurrencyConversionRepository
    {
        Task<int> AddConversionAsync(CurrencyConversion conversion);
        Task<IEnumerable<ConversionReport>> GetConversionReportAsync(DateTime? fromDate, DateTime? toDate, string? personalNumber); 
    }
}
