using GuardianCore.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace GuardianCore.Handler;

public class InternalServerExceptionHandler(ILogger<InternalServerExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not BaseException  internalServerException)
        {
            return false;
        }

        logger.LogError(internalServerException, "Exception occurred {message}", internalServerException.Message);

        var problemDetails = new ProblemDetails
        {
            Title = "Internal Server Error",
            Detail = internalServerException.Message,
            Status = StatusCodes.Status500InternalServerError
        };

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);
        return true;
    }
}
