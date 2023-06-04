using CurrencyExchange.Application.Features.Conversions.Commands;
using CurrencyExchange.Application.Features.Conversions.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversionController : BaseApiController
    {
        [HttpPost("Convert")]
        public async Task<IActionResult> SaveCurrencyConversionAsync(CurrencyConversionCommand command)
        {
            var result = await Mediator.Send(command);
            if(string.IsNullOrEmpty(result.ErrorMessage))
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("Report")]
        public async Task<IActionResult> GetConversionReportAsync([FromQuery] GetConversionsQuery query)
        {
            var result = await Mediator.Send(query);
            if (string.IsNullOrEmpty(result.ErrorMessage))
                return Ok(result);

            return StatusCode(500,result);
        }
    }
}
