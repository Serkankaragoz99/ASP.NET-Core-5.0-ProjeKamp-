﻿using CoreLayer.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using CoreLayer.CrossCuttingConcerns.Logging.Log4Net;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace CoreLayer.Extensions
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;

        private LoggerServiceBase _databaseLoggerServiceBase;

        private LoggerServiceBase _fileLoggerServiceBase;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;

            _databaseLoggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(typeof(DatabaseLogger));

            _fileLoggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(typeof(FileLogger));
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = "Internal Server Error";

            IEnumerable<ValidationFailure> errors;

            if (exception.GetType() == typeof(ValidationException))
            {
                message = exception.Message;
                errors = ((ValidationException)exception).Errors;
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                var exceptionModel = new ValidationErrorDetail
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = message,
                    ValidationErrors = errors
                };

                _databaseLoggerServiceBase.Error(exceptionModel);

                _fileLoggerServiceBase.Error(exceptionModel);

                return httpContext.Response.WriteAsync(exceptionModel.ToString());
            }

            _databaseLoggerServiceBase.Error(exception);
            _fileLoggerServiceBase.Error(exception);

            return httpContext.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }
}
