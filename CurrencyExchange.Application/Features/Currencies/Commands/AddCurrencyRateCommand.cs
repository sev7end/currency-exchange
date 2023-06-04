using AutoMapper;
using CurrencyExchange.Application.DataTransferObjects.Common;
using CurrencyExchange.Application.Entities;
using CurrencyExchange.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Application.Features.Currencies.Commands
{
    public class AddCurrencyRateCommand : IRequest<Response<bool>>
    {
        public int CurrencyId { get; set; }
        public decimal BuyRate { get; set; }
        public decimal SellRate { get; set; }
    }

    public class AddCurrencyRateCommandHandler : IRequestHandler<AddCurrencyRateCommand, Response<bool>>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public AddCurrencyRateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) 
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Response<bool>> Handle(AddCurrencyRateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var currencyRate = this.mapper.Map<CurrencyRate>(request);
                var result = await this.unitOfWork.CurrencyRates.AddAsync(currencyRate);
                unitOfWork.Complete();
                return new Response<bool>(true, "SUCCESS");
            }
            catch (Exception ex)
            {
                return new Response<bool>(false, "ERROR", ex.Message);
            }
        }
    }
}
