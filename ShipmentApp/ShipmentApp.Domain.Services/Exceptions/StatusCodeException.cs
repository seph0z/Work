using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentApp.Domain.Services.Exceptions
{
    public class StatusCodeException : Exception
    {
        public StatusCodeException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
        public HttpStatusCode StatusCode { get; set; }
    }

    public class StatusCodeExceptionHandler
    {
        private readonly RequestDelegate request;

        public StatusCodeExceptionHandler(RequestDelegate pipeline)
        {
            this.request = pipeline;
        }

        public Task Invoke(HttpContext context) => this.InvokeAsync(context);

        async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await this.request(context);
            }
            catch (StatusCodeException exception)
            {
                context.Response.StatusCode = (int)exception.StatusCode;
            }
        }
    }
}
