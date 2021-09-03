using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.Net;
using Utilities.Models;
using Microsoft.Extensions.Hosting;
using Utilities.Exceptions;
using System.Text.Json;

namespace API.middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;


        public ExceptionMiddleware(RequestDelegate next, IWebHostEnvironment webHostEnvironment)
        {
            _next = next;
            _env = webHostEnvironment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                await HandleExceptionAsync(context, ex, _env);
            }

            // Call the next delegate/middleware in the pipeline
            
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex, IWebHostEnvironment env)
        {
            var code = HttpStatusCode.InternalServerError;
            var errors = new ApiErrorResponse() { StatusCode = (int)code };

            if (env.IsDevelopment())
            {
                errors.Details = ex.StackTrace;
            } else
            {
                errors.Details = ex.Message;
            }

            switch(ex)
            {
                case ApplicationValidationException e:
                    errors.Message = e.Message;
                    errors.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    break;
                 default:
                    errors.Message = "Something wrong in our system";
                    break;
            }

            var results = JsonSerializer.Serialize<ApiErrorResponse>(errors);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = errors.StatusCode;


            await context.Response.WriteAsync(results);


        }
    }
}
