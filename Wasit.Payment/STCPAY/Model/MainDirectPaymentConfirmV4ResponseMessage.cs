namespace Wasit.Payment.STCPAY.Model
{
    public class MainDirectPaymentConfirmV4ResponseMessage
    {
        public DirectPaymentConfirmV4ResponseMessage DirectPaymentConfirmV4ResponseMessage { get; set; }
        public int Code { get; set; }
        public string Text { get; set; } = "";
        public int Type { get; set; }
    }
}