using CurrencyExchange.Application.Features.Currencies.Commands;
using CurrencyExchange.Application.Features.Currencies.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : BaseApiController
    {
        [HttpGet("List")]
        public async Task<IActionResult> GetCurrenciesAsync([FromQuery] GetCurrenciesQuery query)=> Ok(await Mediator.Send(query));

        [HttpPost("Add")]
        public async Task<IActionResult> AddCurrencyAsync(AddCurrencyCommand command)
        {
            var result = await Mediator.Send(command);
            if (string.IsNullOrEmpty(result.ErrorMessage))
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("Rate/Add")]
        public async Task<IActionResult> AddCurrencyRateAsync(AddCurrencyRateCommand command)
        {
            var result = await Mediator.Send(command);
            if (string.IsNullOrEmpty(result.ErrorMessage))
                return Ok(result);

            return BadRequest(result);
        }
    }
}
