using FullEquip.Api.Auth.Exceptions;
using FullEquip.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FullEquip.Api.Middleware
{
    public class ApiExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ApiExceptionMiddleware(RequestDelegate next, ILogger<ApiExceptionMiddleware> logger)

        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                var httpStatusCode = ConfigurateExceptionTypes(ex);
                if (httpStatusCode == HttpStatusCode.InternalServerError) _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = (int)httpStatusCode;
                context.Response.ContentType = "application/json";
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    byte[] data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(ex.Message));
                    await context.Response.Body.WriteAsync(data, 0, data.Length);
                }
            }
        }

        private HttpStatusCode ConfigurateExceptionTypes(Exception exception)
        {
            HttpStatusCode httpStatusCode;

            // Exception type To Http Status configuration 
            switch (exception)
            {
                case var _ when exception is ValidationException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    break;
                case var _ when exception is AuthException:
                    httpStatusCode = HttpStatusCode.Unauthorized;
                    break;
                default:
                    httpStatusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            return httpStatusCode;
        }
    }
}
