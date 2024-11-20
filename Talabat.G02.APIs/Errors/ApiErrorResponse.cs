namespace TalabatG02.APIs.Errors
{
    public class ApiErrorResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public ApiErrorResponse(int StatusError, string? message=null)
        {
            this.StatusCode = StatusError;
            Message = message ?? GetDefaultMessageForStatuseCode(StatusCode);
        }
        private string? GetDefaultMessageForStatuseCode(int statuseCode)
        {
            return statuseCode switch
            {
                400 => "A bad Request, you have made",
                401 => "thourized, you are not",
                404 => "Resourses Not Found",
                500 => "There is Serever Error",
                _ => null
            };
        }
    }
}
