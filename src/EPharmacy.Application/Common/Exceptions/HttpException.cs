namespace EPharmacy.Application.Common.Exceptions;

public class HttpException : Exception
{
    public HttpException(string error) : base(error)
    {
    }

    public HttpException(string error, int statusCode) : base($"{error}##{statusCode}")
    {
    }
}
