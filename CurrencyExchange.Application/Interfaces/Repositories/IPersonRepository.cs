using CurrencyExchange.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Application.Interfaces.Repositories
{
    public interface IPersonRepository
    {
        Task<int> AddAsync(Person person);
        Task<Person> GetAsync(int id);
        Task<bool> GetPersonLimitAsync(string personalNumber,decimal initialAmount);
    }
}
