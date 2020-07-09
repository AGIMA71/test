using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using ABNLookup.Validation;
using ABNLookup.Interfaces;
using Microsoft.Extensions.Configuration;
using ABNLookup.Settings;
using ABNLookup.Constants;

namespace ABNLookup.Middlewares
{
    // REF: https://code-maze.com/global-error-handling-aspnetcore
    public class ExceptionHandler
    {
        private readonly IWebHostEnvironment environment;
        private readonly RequestDelegate next;
        private readonly IEmailService emailService;
        private readonly IConfiguration _configuration;

        public ExceptionHandler(IWebHostEnvironment env, RequestDelegate next, IEmailService emailService, IConfiguration configuration) =>
            (this.environment, this.next, this.emailService, _configuration) = (env, next, emailService, configuration);

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            AppSettings appSettings = _configuration
              .GetSection(AbnLookupConstants.AppSettings)
              .Get<AppSettings>();

            var statusCode = StatusCodes.Status500InternalServerError;
            object errorObject = new { error = "Something went wrong while processing the request." };
            ProcessMessage unexpectedError = appSettings.DisplayExceptionDetails? new ProcessMessage(ValidationConstants.ModelValidationErrorCode, 
                "An unexpected error ocurred, please contact the ATO." + "Exception: "  +  exception.Message + "Callstack: " + exception.StackTrace.Trim()):
                new ProcessMessage(ValidationConstants.ModelValidationErrorCode,
                "An unexpected error ocurred, please contact the ATO.");

            // this block will be removed later once logging is configured and the exception will be written to the log instead    
            switch(exception)
            {
                default:
                    if (environment.IsDevelopment())
                    {
                        unexpectedError.Description = $"{exception.Message} - {exception.StackTrace.Trim()}";
                    }
                    else
                    {
                        emailService.SendAsync(exception);
                    }
                    break;
            }

            string result = JsonSerializer.Serialize(unexpectedError);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(result);
        }
    }
}