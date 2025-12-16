
using LinkDev.Talabat.APIs.Controllers;
using LinkDev.Talabat.APIs.Controllers.Errors;
using LinkDev.Talabat.APIs.Controllers.Middlewares;
using LinkDev.Talabat.APIs.Extensions;
using LinkDev.Talabat.Application;
using LinkDev.Talabat.Domain.Contracts;
using LinkDev.Talabat.Infrastructure.Persistence;
using LinkDev.Talabat.Infrastructure;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using LinkDev.Talabat.Infrastructure.Persistence.Data.Seeds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using LinkDev.Talabat.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using LinkDev.Talabat.Domain.Contracts.Persistence;
using LinkDev.Talabat.Infrastructure.Persistence.Identity;

namespace LinkDev.Talabat.APIs
{
    public class Program
    {
        // Entry Point
        public static async Task Main(string[] args)
        {
            var webApplicationBuilder = WebApplication.CreateBuilder(args);

            #region Configure Service
            // Add services to the container.

            webApplicationBuilder.Services
                .AddControllers() // Register Required Services by ASP.NET Core Web APIs to DI Container
                .AddApplicationPart(typeof(AssemblyControllersInformation).Assembly) // To make sure that Controllers from other assemblies are registered, but it already see it by default.
                .ConfigureApiBehaviorOptions((setupAction) =>
                {
                    setupAction.SuppressModelStateInvalidFilter = false;
                    // Will not execute any endpoint that has invalid model state
                    setupAction.InvalidModelStateResponseFactory = (actionContext) =>
                    {
                        var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count > 0)
                                       .Select(p => new ApiValidationErrorResponse.ValidationError()
                                       {
                                           Field = p.Key,
                                           Errors = p.Value.Errors.Select(p => p.ErrorMessage)
                                       });

                        return new BadRequestObjectResult(new ApiValidationErrorResponse()
                        {
                            Errors = errors
                        });
                    };

                })
                .AddNewtonsoftJson(options =>
                {
                    // Why??
                    /// if i have navigation properties that reference each other
                    /// like Category -> Products -> Category -> Products ...
                    /// it will cause circular reference exception
                    /// so we need to ignore it to avoid this exception
                    /// that when i come product in category it will ignore category property and also exchange

                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            webApplicationBuilder.Services.AddEndpointsApiExplorer();
            webApplicationBuilder.Services.AddSwaggerGen();
            webApplicationBuilder.Services.AddInfrastructureServices(webApplicationBuilder.Configuration);
            webApplicationBuilder.Services.AddPersistenceServices(webApplicationBuilder.Configuration);
            webApplicationBuilder.Services.AddApplicationServices();
            webApplicationBuilder.Services.AddIdentityServices(webApplicationBuilder.Configuration);

            #endregion

            var app = webApplicationBuilder.Build();

            #region Database Initialization & Seeding

            await app.InitializeDbAsync();

            #endregion


            #region Configure Kestrel Middlewates
            // Configure the HTTP request pipeline.

            // Routing Middleware should be placed before any middleware that depends on routing decisions

            app.UseMiddleware<ExceptionHandlerMiddleware>();


            #region Middleware
            /// app.Use(async (context, next) =>
            /// {
            ///     using var scope = app.Services.CreateAsyncScope();
            ///     var services = scope.ServiceProvider;
            /// 
            ///     var environment = services.GetRequiredService<IWebHostEnvironment>();
            /// 
            ///     var logger = services.GetRequiredService<ILogger>();
            /// 
            ///     try
            ///     {
            ///         // Logic Will be executed for the request
            /// 
            ///         await next(context); // Go to next Middleware of the application itself
            /// 
            ///         // Logic will be executed for the response
            /// 
            ///     }
            ///     catch (Exception ex)
            ///     {
            ///         #region Logging: ToDo with Serial Package
            /// 
            ///         if (environment.IsDevelopment())
            ///         {
            ///             logger.LogError(ex, ex.Message, ex.StackTrace!.ToString());
            ///         }
            ///         else
            ///         {
            ///             // Log Exception in External Resource like Database or File(Text, Json)
            ///         }
            /// 
            ///         #endregion
            /// 
            ///         context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            ///         context.Response.ContentType = "application/json";
            /// 
            ///         var response = environment.IsDevelopment() ? new ApiExceptionResponse((int)///H ttpStatusCode.InternalServerError, ex.Message, ex.StackTrace?.ToString()) : new ApiExceptionResponse/((int)//H ttpStatusCode.InternalServerError, ex.Message);
            /// 
            ///         await context.Response.WriteAsync(response.ToString());
            /// 
            ///     }
            /// 
            /// }); 
            #endregion

            app.UseRouting();

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseStatusCodePagesWithReExecute("/Errors/{0}");
            //app.UseStatusCodePagesWithRedirects("/Errors/{0}");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                //app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            #endregion

            app.Run();
        }
    }
}
