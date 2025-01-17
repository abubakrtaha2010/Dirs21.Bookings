﻿namespace Dirs21.Bookings.API.Middlewares;

/// <summary>
/// Middleware to handle exceptions and provide a consistent error response format.
/// </summary>
/// <param name="next">The next middleware in the pipeline.</param>
/// <param name="logger">The logger to log error details.</param>
public class ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
{
    private readonly JsonSerializerOptions _serializationOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public async Task InvokeAsync(HttpContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        try
        {
            await next(context).ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            ErrorResponse? error;

            switch (exception)
            {
                case GetMappingException:
                    error = JsonSerializer.Deserialize<ErrorResponse>(exception.Message);
                    if (error?.ErrorType is not ErrorType.ResourceNotFound)
                    {
                        LoggerMessage
                            .Define<string>(LogLevel.Error, LogEvents.ExceptionFailureId, "{Message}")
                            .Invoke(logger, exception.Message, exception);
                    }
                    break;

                case SaveMappingException:
                case MapDataException:
                    error = JsonSerializer.Deserialize<ErrorResponse>(exception.Message);

                    LoggerMessage
                        .Define<string>(LogLevel.Error, LogEvents.ExceptionFailureId, "{Message}")
                        .Invoke(logger, exception.Message, exception);
                    break;

                default:
                    error = new ErrorResponse
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        ErrorType = ErrorType.Other,
                        UserMessage = "Internal server error occured.",
                        InternalMessage = exception.Message
                    };

                    LoggerMessage
                        .Define<string>(LogLevel.Error, LogEvents.ExceptionFailureId, "{Message}")
                        .Invoke(logger, JsonSerializer.Serialize(error, _serializationOptions), exception);
                    break;
            }

            context.Response.StatusCode = (int)(error?.StatusCode ?? HttpStatusCode.InternalServerError);
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(JsonSerializer.Serialize(error, _serializationOptions)).ConfigureAwait(false);
        }
    }
}
