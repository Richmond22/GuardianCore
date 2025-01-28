using System.Net;

namespace GuardianCore.Exceptions;

public abstract class BadRequestException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : BaseException(message, statusCode)
{
    
}