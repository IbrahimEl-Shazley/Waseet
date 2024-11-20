namespace Wasit.Payment.STCPAY.Model
{
    public class DirectPaymentConfirmV4RequestMessage
    {
        public string OtpReference { get; set; }
        public string OtpValue { get; set; }
        public string STCPayPmtReference { get; set; }
        public string TokenReference { get; set; }
        public string TokenizeYn { get; set; }
    }
}