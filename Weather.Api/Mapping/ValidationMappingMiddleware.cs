﻿using FluentValidation;
using Weather.Api.Responses;

namespace Weather.Api.Mapping;

public class ValidationMappingMiddleware
{

    private readonly RequestDelegate _next;


    public ValidationMappingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            context.Response.StatusCode = 400;
            var validationFailureResponse = new ValidationFailureResponse
            {
                Errors = ex.Errors.Select(x => new validationResponse
                {
                    PropertyName = x.PropertyName,
                    Message = x.ErrorMessage
                })
            };
            await context.Response.WriteAsJsonAsync(validationFailureResponse);
        }
    }

}
