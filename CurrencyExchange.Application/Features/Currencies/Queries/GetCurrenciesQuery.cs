using AutoMapper;
using CurrencyExchange.Application.DataTransferObjects.Common;
using CurrencyExchange.Application.DataTransferObjects.Responses;
using CurrencyExchange.Application.Entities;
using CurrencyExchange.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Application.Features.Currencies.Queries
{
    public class GetCurrenciesQuery : IRequest<Response<IEnumerable<CurrencyInfoResponse>>>
    {

    }
    public class GetCurrenciesQueryHandler : IRequestHandler<GetCurrenciesQuery, Response<IEnumerable<CurrencyInfoResponse>>>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public GetCurrenciesQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) 
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Response<IEnumerable<CurrencyInfoResponse>>> Handle(GetCurrenciesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await this.unitOfWork.Currencies.GetAllAsync();
                var response = this.mapper.Map<IEnumerable<CurrencyInfo>, IEnumerable<CurrencyInfoResponse>>(result);
                unitOfWork.Complete();
                return new Response<IEnumerable<CurrencyInfoResponse>>(response, "SUCCESS");
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<CurrencyInfoResponse>>(null, "ERROR", ex.Message);
            }
        }
    }
}
