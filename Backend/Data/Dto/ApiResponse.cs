using Microsoft.AspNetCore.Mvc;

namespace Backend.Data.Dto;

public static class ApiResponse
{
    public static ObjectResult Success(object? data = null, int statusCode = StatusCodes.Status200OK, string? message = null)
    {
        var response = new ObjectResult(new
        {
            message,
            data,
        })
        {
            StatusCode = statusCode
        };

        return response;
    }

    public static ObjectResult Fail(string message, int statusCode = StatusCodes.Status500InternalServerError, Dictionary<string, string[]?>? errors = null, object? data = null)
    {
        var response = new ObjectResult(new
        {
            message,
            errors,
            data,
        })
        {
            StatusCode = statusCode
        };

        return response;
    }
}
