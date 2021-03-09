namespace Futbal.Mng.Api.Models
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }
    
        public static ErrorDetails Create(int statusCode, string message) 
            => new ErrorDetails
                    {
                        StatusCode = statusCode,
                        Message = message
                    };

        public static string CreateAsString(int statusCode, string message)
            => Create(statusCode, message)
                .ToString();
        
        public override string ToString()
        {
            return $"Status Code: {StatusCode.ToString()}, Something went wrong: {Message}";     
        }
    }
}