namespace Backend.Shared.Responses
{
    public class ApiResponse<T>
    {
        public bool Success { get; init; }
        public string Message { get; set; } = String.Empty;
        public T? Data { get; init; }
        public IEnumerable<string>? Errors { get; set; }

        public static ApiResponse<T> Ok(T? data, string message = "")
            => new() { Success = true, Data = data, Message = message };

        public static ApiResponse<T> Fail(IEnumerable<string>? errors, string message = "")
            => new() { Success = false, Message = message, Errors = errors };
    }
    public class ApiResponse
    {
        public bool Success { get; init; }
        public string Message { get; set; } = String.Empty;
        public IEnumerable<string>? Errors { get; set; }

        public static ApiResponse Ok(string message = "")
            => new() { Success = true, Message = message };

        public static ApiResponse Fail(IEnumerable<string>? errors, string message = "")
            => new() { Success = false, Message = message, Errors = errors };
    }
}
