namespace Talabat.Pl.Error
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse(int StatusCode,string Message=null)
        {
            this.StatusCode = StatusCode;
            this.Message= Message?? GenerateErrorMessage(StatusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        private string GenerateErrorMessage(int StatusCode)
        => StatusCode switch
        {
            400 => "A bad Request ,You have made",
            401 => "Authorized, You are not!",
            404 => "Resource was not found",
            500 => "Errors are the path to the dark side,Errors lead to Anger." +
                   "Anger leads to hate.Hate leads to career change!!",
            _ => null
        };
        
    }
}
