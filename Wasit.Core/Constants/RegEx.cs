namespace Wasit.Core.Constants
{
    public static class RegEx
    {
        public const string SaudiPhone = @"^(5)(5|0|3|6|4|9|1|8|7)([0-9]{7})$";
        public const string Email = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]{2,}@[a-zA-Z0-9]+(?:[a-zA-Z0-9-]*[a-zA-Z0-9]\.)+[a-zA-Z]{2,}$";
        public const string Vat = @"^(100(\.0+)?|([1-9]?[0-9])(\.[0-9]+)?)$";
        public const string Price = @"^(0*[1-9]\d*(\.\d+)?|0+\.\d*[1-9]\d*)$";
        public const string IntegerNumber = @"^[1-9]\d*$";
        public const string SaudiNationalID = @"^[1|2]{1}[0-9]{9}$";
        public const string IBAN = @"^SA\d{2}[0-9]{20}$";
        public const string SaudiBankAccountNo = @"^\d{18}$";
        public const string Url = @"https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9]+\.[^\s]{2,}|www\.[a-zA-Z0-9]+\.[^\s]{2,}";
        public const string SaudiCommercialNo = @"^\d{10}$";
    }
}
