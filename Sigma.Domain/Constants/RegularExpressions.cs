namespace Beaver.Domain.Constants
{
    public static class RegularExpressions
    {
        public const string MailFormat = @"(\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)";
        public const string UrlFormat = @"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=]*)?$";
        public const string PhoneNumberFormat = @"^\+996[0-9]{9}$";
    }
}
