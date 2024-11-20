namespace Wasit.Payment.STCPAY.Model
{
    public class DirectPaymentAuthorizeV4ResponseMessage
    {
        public string OtpReference { get; set; }
        public string STCPayPmtReference { get; set; }
        public int ExpiryDuration { get; set; }
    }
}