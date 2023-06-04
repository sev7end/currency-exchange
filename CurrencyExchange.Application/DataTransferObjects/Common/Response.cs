using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Application.DataTransferObjects.Common
{
    public class Response<T> 
    {
        public string ErrorMessage { get; set; }
        public string ResponseMessage { get; set; }
        public T? Data { get; set; }

        public Response(T? data, string responseMessage)
        {
            this.Data = data;
            this.ResponseMessage = responseMessage;
        }

        public Response(T? data,string responseMessage, string errorMessage) {
            this.Data = data;
            this.ResponseMessage = responseMessage;
            this.ErrorMessage = errorMessage;
        }
    }
}
