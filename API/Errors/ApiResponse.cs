namespace API.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode); // If message is null, assign own message depending on status code
        }
        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            if (statusCode == 400)
            {
                return "Bad request has been made";
            }
            else if (statusCode == 401)
            {
                return "You are not Authorized for this action";
            }
            else if (statusCode == 404)
            {
                return "Resource was not found";
            }
            else if (statusCode == 500)
            {
                return "Internal Server Error";
            }
            else
            {
                return null;
            }
        }
    }
}