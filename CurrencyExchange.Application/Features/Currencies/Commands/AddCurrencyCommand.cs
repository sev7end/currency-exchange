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
    public class AddCurrencyCommand : IRequest<Response<bool>>
    {
        public string Code { get; set; }
        public string NameKa { get; set; }
    }
    public class AddCurrencyCommandHandler : IRequestHandler<AddCurrencyCommand, Response<bool>>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public AddCurrencyCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Response<bool>> Handle(AddCurrencyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var currency = this.mapper.Map<Currency>(request);
                var result = await unitOfWork.Currencies.AddAsync(currency);
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
