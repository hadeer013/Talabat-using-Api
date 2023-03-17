using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Pl.Error;

namespace Talabat.Pl.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment environment;

        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger,IHostEnvironment environment)
        {
            this.next = next;
            this.logger = logger;
            this.environment = environment;
        }
        public async Task InvokeAsync(HttpContext Context)
        {
            try
            {
                await next(Context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                Context.Response.ContentType= "application/json";
                Context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var responseMessage = environment.IsDevelopment()
                    ? new ExceptionErrorResponse(ex.Message, ex.StackTrace.ToString())
                    : new ExceptionErrorResponse();
                var options=new JsonSerializerOptions() { PropertyNamingPolicy= JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(responseMessage, options);
                await Context.Response.WriteAsync(json);
            }
        }
    }
}
