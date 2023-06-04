using AutoMapper;
using CurrencyExchange.Application.DataTransferObjects.Common;
using CurrencyExchange.Application.DataTransferObjects.Responses;
using CurrencyExchange.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Application.Features.Conversions.Queries
{
    public class GetConversionsQuery : IRequest<Response<IEnumerable<ConversionResponse>>>
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? PersonalNumber { get; set; }
    }
    public class GetConversionsQueryHandler : IRequestHandler<GetConversionsQuery, Response<IEnumerable<ConversionResponse>>>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public GetConversionsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) 
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Response<IEnumerable<ConversionResponse>>> Handle(GetConversionsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await this.unitOfWork.CurrencyConversions.GetConversionReportAsync(request.StartDate, request.EndDate, request.PersonalNumber);
                var response = this.mapper.Map<IEnumerable<ConversionResponse>>(result);
                unitOfWork.Complete();
                return new Response<IEnumerable<ConversionResponse>>(response, "SUCCESS");
            }
            catch(Exception ex)
            {
                return new Response<IEnumerable<ConversionResponse>>(null, "ERROR", ex.Message);
            }
        }
    }
}
