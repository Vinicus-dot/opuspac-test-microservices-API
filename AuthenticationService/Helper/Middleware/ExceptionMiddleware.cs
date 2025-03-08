using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Host;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace AuthenticationService.Helper.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        private readonly List<int> StatusCantWriteOnResponse =
        [
           StatusCodes.Status204NoContent,
        ];
        public async Task InvokeAsync(HttpContext context)
        {
            Stream originalBody = context.Response.Body;

            try
            {
                using (var swapStream = new MemoryStream())
                {
                    var originalResponseBody = context.Response.Body;

                    context.Response.Body = swapStream;
                    await _next(context);

                    swapStream.Seek(0, SeekOrigin.Begin);
                    string responseBody = new StreamReader(swapStream).ReadToEnd();
                    swapStream.Seek(0, SeekOrigin.Begin);

                    await swapStream.CopyToAsync(originalResponseBody);
                    context.Response.Body = originalResponseBody;
                    if (context.Request.Path.StartsWithSegments("/api"))
                        _logger.LogInformation(context.Request.Method + context.Request.Path + " " + responseBody);
                }
            }
            catch (Exception e)
            {
                context.Response.Body = originalBody;
                context.Response.ContentType = "application/json";
                var (message, statusCode) = GetStatusCodeAndMessage(e);
                context.Response.StatusCode = (int)statusCode;
                var error = new
                {
                    StatusCode = context.Response.StatusCode.ToString(),
                    Message = message
                };
                _logger.LogError($"Error: {error.StatusCode} - {error.Message}");

                if (!StatusCantWriteOnResponse.Contains(context.Response.StatusCode))
                    await context.Response.WriteAsync(JsonSerializer.Serialize(error));
            }
        }

        private (string, HttpStatusCode) GetStatusCodeAndMessage(Exception ex)
        {
            return ex switch
            {
                WebException webEx when webEx.Response is HttpWebResponse httpResponse => (httpResponse.StatusDescription, httpResponse.StatusCode),
                HttpException httpException => (httpException.StatusDescription, (HttpStatusCode)Enum.ToObject(typeof(HttpStatusCode), httpException.StatusCode)),
                ArgumentException or InvalidOperationException or BadHttpRequestException or DivideByZeroException or DllNotFoundException => (ex.Message, HttpStatusCode.BadRequest),
                _ => (ex.Message, HttpStatusCode.InternalServerError),
            };
        }
    }
}
