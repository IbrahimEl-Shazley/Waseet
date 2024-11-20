namespace Wasit.Core.Helpers.General
{
    public static class OTPHelper
    {
        public static string OTP(int size = 4)
        {
            Random random = new Random();
            string otp = "";
            for (int i = 0; i < size; i++)
            {
                otp += random.Next(0, 9).ToString();
            }
            return otp;
        }
    }
}
