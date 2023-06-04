using CurrencyExchange.Application.Entities;
using CurrencyExchange.Application.Interfaces.Repositories;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CurrencyExchange.Infrastructure.Implementations.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IDbTransaction _transaction;
        public PersonRepository(IDbTransaction transaction)
        {
            _transaction = transaction;
        }

        public async Task<int> AddAsync(Person person)
        {
            var sql = "EXEC dbo.spAddPerson @PersonalNumber, @FirstName, @LastName";
            var parameters = new { PersonalNumber = person.PersonalNumber, FirstName = person.FirstName, LastName = person.LastName};

            return await _transaction.Connection.QuerySingleOrDefaultAsync<int>(sql, parameters, _transaction);
        }

        public async Task<Person> GetAsync(int id)
        {
            var sql = "EXEC [dbo].[spGetPerson] @Id";
            var parameters = new { Id = id };

            return await _transaction.Connection.QuerySingleOrDefaultAsync<Person>(sql, parameters, _transaction);
        }

        public async Task<bool> GetPersonLimitAsync(string personalNumber, decimal initialAmount)
        {
            var sql = "EXEC [dbo].[spGetPersonConversionLimit] @PersonalNumber, @InitialAmount";
            var parameters = new { PersonalNumber = personalNumber, InitialAmount = initialAmount };

            return await _transaction.Connection.QuerySingleOrDefaultAsync<bool>(sql, parameters, _transaction);
        }

    }
}
