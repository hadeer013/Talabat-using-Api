namespace Talabat.Pl.Error
{
    public class ExceptionErrorResponse:ApiErrorResponse
    {
        public ExceptionErrorResponse(string Message=null ,string Details=null) : base(500,Message)
        {
            this.Details=Details;
        }
        public string Details { get; set; }

    }
}
