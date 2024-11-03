using Blog.API.Models.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Blog.API.Commons
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse()
        {
            StatusCode = StatusCodes.Status400BadRequest;
            StatusPhrase = StatusCodes.Status400BadRequest.ToString();
            ErrorID = Guid.NewGuid();
            Errors = new List<string>();
            Timestamp = DateTime.UtcNow;
        }

        public ApiErrorResponse(int StatusCode, string StatusPhrase, List<string> Errors)
        {
            this.StatusCode = StatusCode;
            this.StatusPhrase = StatusPhrase;
            ErrorID = Guid.NewGuid();
            this.Errors = Errors;
            Timestamp = DateTime.UtcNow;
        }

        public ApiErrorResponse(int StatusCode, string StatusPhrase, string Error)
        {
            this.StatusCode = StatusCode;
            this.StatusPhrase = StatusPhrase;
            ErrorID = Guid.NewGuid();
            Errors = new List<string> { Error };
            Timestamp = DateTime.UtcNow;
        }

        public ApiErrorResponse(int StatusCode, string StatusPhrase, Guid ErrorID, string Error)
        {
            this.StatusCode = StatusCode;
            this.StatusPhrase = StatusPhrase;
            this.ErrorID = ErrorID;
            Errors = new List<string> { Error };
            Timestamp = DateTime.UtcNow;
        }

        public int StatusCode { get; set; }
        public string StatusPhrase { get; set; }
        public Guid ErrorID { get; set; }
        public List<string> Errors { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
