namespace Backend.Exceptions
{
    public class ApiException: Exception
    {
        public int StatusCode { get; }
        public IEnumerable<string>? Errors { get;}
        public ApiException(int statusCode = 500, string message = "", IEnumerable<string>? errors = null) : base(message)
        {
            StatusCode = statusCode;
            Errors = errors;
        }
    }
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }

    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
    }
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message) { }
    }
}
