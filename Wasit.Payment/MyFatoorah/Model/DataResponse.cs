namespace Wasit.Payment.MyFatoorah.Model
{
    public class DataResponse
    {
        public int InvoiceId { get; set; }
        public bool IsDirectPayment { get; set; }
        public string PaymentURL { get; set; }
        public string CustomerReference { get; set; }
        public string UserDefinedField { get; set; }
    }
}
