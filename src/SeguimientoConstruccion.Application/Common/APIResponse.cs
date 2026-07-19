namespace SeguimientoConstruccion.Application.Common
{
    public class APIResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public int StatusCode { get; set; }

        public static APIResponse<T> SuccessResponse(T data, int statusCode = 200)
        {
            return new APIResponse<T>
            {
                Success = true,
                Message = string.Empty,
                Data = data,
                StatusCode = statusCode
            };
        }

        public static APIResponse<T> ErrorResponse(string message, int statusCode = 400)
        {
            return new APIResponse<T>
            {
                Success = false,
                Message = message,
                Data = default,
                StatusCode = statusCode
            };
        }
    }
}
