namespace Wasit.Payment.MyFatoorah.Model
{
    public class ExecutePaymentResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object ValidationErrors { get; set; }
        public DataResponse Data { get; set; }
    }
}
