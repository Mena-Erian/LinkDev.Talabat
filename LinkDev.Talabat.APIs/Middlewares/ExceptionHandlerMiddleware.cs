using LinkDev.Talabat.APIs.Controllers.Errors;
using LinkDev.Talabat.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Middlewares
{
    // Convention-Based
    internal class ExceptionHandlerMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext httpContext, ILogger<ExceptionHandlerMiddleware> logger, IWebHostEnvironment environment)
        {
            try
            {
                // Logic Will be executed for the request

                await next(httpContext); // Go to next Middleware of the application itself

                // Logic will be executed for the response

               /// if (httpContext.Response.StatusCode == (int)HttpStatusCode.NotFound)
               /// {
               ///     var response = new ApiErrorResponse((int)HttpStatusCode.NotFound, $"The requested endpoint: ///{httpContext.Request.Path} is not Found");
               ///
               ///
               ///     var json = JsonSerializer.Serialize(response, new JsonSerializerOptions()
               ///     {
               ///         PropertyNamingPolicy = JsonNamingPolicy.CamelCase
               ///     });
               ///
               ///     await httpContext.Response.WriteAsync(json.ToString());
               /// }

            }
            catch (Exception ex)
            {
                #region Logging: ToDo with Serial Package

                if (environment.IsDevelopment())
                {
                    logger.LogError(ex, ex.Message, ex.StackTrace!.ToString());
                }
                else
                {
                    // Log Exception in External Resource like Database or File(Text, Json)
                }

                #endregion

                await HandleExceptionAsync(httpContext, environment, ex);

            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, IWebHostEnvironment environment, Exception ex)
        {
            int statusCode;
            switch (ex)
            {
                case NotFoundException:
                    statusCode = (int)HttpStatusCode.NotFound;
                    break;

                case BadRequestException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;

                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var response = environment.IsDevelopment() ? new ApiExceptionResponse(statusCode, ex.Message, ex.StackTrace?.ToString()) : new ApiExceptionResponse(statusCode, ex.Message);

            await context.Response.WriteAsync(response.ToString());
        }
    }


    /// // Factory-Based
    /// internal class ExceptionHandlerMiddleware : IMiddleware
    /// {
    ///     public Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
    ///     {
    ///         throw new NotImplementedException();
    ///     }
    /// }
}
