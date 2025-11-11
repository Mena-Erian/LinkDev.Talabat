using LinkDev.Talabat.APIs.Controllers.Errors;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Middlewares
{
    // Convention-Based
    internal class ExceptionHandlerMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context, ILogger<ExceptionHandlerMiddleware> logger, IWebHostEnvironment environment)
        {
            try
            {
                // Logic Will be executed for the request

                await next(context); // Go to next Middleware of the application itself

                // Logic will be executed for the response

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

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = environment.IsDevelopment() ? new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace?.ToString()) : new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message);

                await context.Response.WriteAsync(response.ToString());

            }
        }
    }


    /// // Factory-Based
    /// internal class ExceptionHandlerMiddleware : IMiddleware
    /// {
    ///     public Task InvokeAsync(HttpContext context, RequestDelegate next)
    ///     {
    ///         throw new NotImplementedException();
    ///     }
    /// }
}
