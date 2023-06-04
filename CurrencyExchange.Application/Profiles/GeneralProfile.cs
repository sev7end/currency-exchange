using AutoMapper;
using CurrencyExchange.Application.DataTransferObjects.Responses;
using CurrencyExchange.Application.Entities;
using CurrencyExchange.Application.Features.Currencies.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Application.Profiles
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<ConversionReport, ConversionResponse>().ReverseMap();
            CreateMap<Currency, AddCurrencyCommand>().ReverseMap();
            CreateMap<CurrencyRate, AddCurrencyRateCommand>().ReverseMap();
            CreateMap<CurrencyInfo, CurrencyInfoResponse>().ReverseMap();
            //CreateMap<IEnumerable<CurrencyInfo>, IEnumerable<CurrencyInfoResponse>>().ReverseMap();
            //CreateMap<IEnumerable<ConversionReport>, IEnumerable<ConversionResponse>>().ReverseMap();

        }
    }
}
