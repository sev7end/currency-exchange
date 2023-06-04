using CurrencyExchange.Application.DataTransferObjects.Common;
using CurrencyExchange.Application.Entities;
using CurrencyExchange.Application.Enums;
using CurrencyExchange.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Application.Features.Conversions.Commands
{
    public class CurrencyConversionCommand : IRequest<Response<bool>>
    {
        public int FromCurrencyId { get; set; }
        public int ToCurrencyId { get; set; }
        public decimal InitialAmount { get; set; }
        public string PersonalNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RecommendatorPersonalNumber { get; set; }
    }
    public class CurrencyConversionCommandHandler : IRequestHandler<CurrencyConversionCommand, Response<bool>>
    {
        private readonly IUnitOfWork unitOfWork;

        public CurrencyConversionCommandHandler(
            IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Response<bool>> Handle(CurrencyConversionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                

                if (request.FromCurrencyId != (int)BaseCurrency.GEL && request.ToCurrencyId != (int)BaseCurrency.GEL)
                    throw new Exception("INVALID_CURRENCIES_PROVIDED");

                if(request.FromCurrencyId == (int)BaseCurrency.GEL)
                {
                    if (!await CheckDailyLimit(request))
                        throw new Exception("DAILY_LIMIT_EXCEEDED");

                    var personId = request.InitialAmount > 3000 ? await GetPersonId(request) : 1;
                    var exchangeRate = await this.unitOfWork.CurrencyRates.GetLatestCurrencyRateAsync(request.ToCurrencyId);
                    
                    var convertedAmount = request.InitialAmount / exchangeRate.BuyRate;

                    await this.unitOfWork.CurrencyConversions.AddConversionAsync(new CurrencyConversion()
                    {
                        ConvertedAmount = convertedAmount,
                        PersonId = personId,
                        FromCurrencyId = request.FromCurrencyId,
                        ToCurrencyId = request.ToCurrencyId,
                        InitialAmount = request.InitialAmount,
                        Rate = exchangeRate.BuyRate,
                    });

                    this.unitOfWork.Complete();
                    return new Response<bool>(true, "SUCCESS");
                }
                else
                {
                    var exchangeRate = await this.unitOfWork.CurrencyRates.GetLatestCurrencyRateAsync(request.FromCurrencyId);
                    var convertedAmount = request.InitialAmount * exchangeRate.SellRate;

                    var personId = convertedAmount > 3000 ? await GetPersonId(request) : 1;
                    
                    await this.unitOfWork.CurrencyConversions.AddConversionAsync(new CurrencyConversion()
                    {
                        ConvertedAmount = convertedAmount,
                        PersonId = personId,
                        FromCurrencyId = request.FromCurrencyId,
                        ToCurrencyId = request.ToCurrencyId,
                        InitialAmount = request.InitialAmount,
                        Rate = exchangeRate.SellRate,
                    });

                    this.unitOfWork.Complete();
                    return new Response<bool>(true, "SUCCESS");
                }
            }
            catch (Exception ex)
            {
                return new Response<bool>(false, "ERROR", ex.Message);
            }
        }
        public async Task<int> GetPersonId(CurrencyConversionCommand request) 
        {
            if(string.IsNullOrEmpty(request.FirstName) || string.IsNullOrEmpty(request.LastName) || string.IsNullOrEmpty(request.PersonalNumber))
            {
                throw new Exception("MISSING_PERSON_INFO");
            }
            return await this.unitOfWork.Persons.AddAsync(new Person()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PersonalNumber = request.PersonalNumber
            });
        }

        public async Task<bool> CheckDailyLimit(CurrencyConversionCommand request)
        {
            var res = await this.unitOfWork.Persons.GetPersonLimitAsync(request.PersonalNumber, request.InitialAmount)
                && await this.unitOfWork.Persons.GetPersonLimitAsync(request.RecommendatorPersonalNumber, request.InitialAmount);
            return res;
        }
    }
}
