namespace API.Errors
{
    public class ApiException : ApiResponse
    {
        public string Details { get; set; }
        public ApiException(int statusCode, string message = null, string Details = null) : base(statusCode, message)
        {
            this.Details = Details;
        }
    }
}