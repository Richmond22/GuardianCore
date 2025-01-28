using GuardianCore.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;


namespace GuardianCore.Handler;

public class BadRequestExceptionHandler(ILogger<BadRequestExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
            Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not BadRequestException badRequestException)
        {
            return false;
        }

        logger.LogError(badRequestException, "Exception occurred {message}", badRequestException.Message);

        var problemDetails = new ProblemDetails
        {
            Title = "Bad Request",
            Detail = badRequestException.Message,
            Status = StatusCodes.Status400BadRequest
        };

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);
        return true;
    }
}
