using System.Net;

namespace GuardianCore.Exceptions;

public abstract class NotFoundException(string message, HttpStatusCode statusCode = HttpStatusCode.NotFound) : BaseException(message, statusCode)
{

}