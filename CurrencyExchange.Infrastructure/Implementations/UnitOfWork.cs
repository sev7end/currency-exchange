using CurrencyExchange.Application.Interfaces.Repositories;
using CurrencyExchange.Application.Interfaces;
using CurrencyExchange.Infrastructure.Implementations.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Infrastructure.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _connection;
        private IDbTransaction _transaction;

        private ICurrencyRepository _currencies;
        private IPersonRepository _persons;
        private ICurrencyConversionRepository _currencyConversions;
        private ICurrencyRateRepository _currencyRates;

        public UnitOfWork(IDbConnection connection)
        {
            _connection = connection;
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public ICurrencyRepository Currencies => _currencies ??= new CurrencyRepository(_transaction);
        public IPersonRepository Persons => _persons ??= new PersonRepository(_transaction);
        public ICurrencyConversionRepository CurrencyConversions => _currencyConversions ??= new CurrencyConversionRepository(_transaction);
        public ICurrencyRateRepository CurrencyRates => _currencyRates ??= new CurrencyRateRepository(_transaction);

        public void Complete()
        {
            try
            {
                _transaction.Commit();
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
                _connection.Close();
            }
        }

        public void Dispose()
        {
            _transaction?.Rollback();
            _transaction?.Dispose();
            _connection?.Dispose();
        }
    }

}
