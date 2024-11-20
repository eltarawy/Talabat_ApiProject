namespace TalabatG02.APIs.Errors
{
    public class ApiExeptionResponse : ApiErrorResponse
    {
        public string? Details { get; set; }

        public ApiExeptionResponse(int statusCode, string? Massage = null, string? Details = null) :base(statusCode)
        {
            this.Details = Details;
            
        }
    }
}
