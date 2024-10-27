namespace Blog.API.Commons
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse()
        {
            StatusCode = StatusCodes.Status400BadRequest;
            StatusPhrase = StatusCodes.Status400BadRequest.ToString();
            Errors = new List<string>();
            Timestamp = DateTime.UtcNow;
        }

        public ApiErrorResponse(int StatusCode, string StatusPhrase, List<string> Errors)
        {
            this.StatusCode = StatusCode;
            this.StatusPhrase = StatusPhrase;
            this.Errors = Errors;
            this.Timestamp = DateTime.UtcNow;
        }

        public int StatusCode { get; set; }
        public string StatusPhrase { get; set; }
        public List<string> Errors { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
