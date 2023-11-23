using System;
using System.Text.RegularExpressions;

namespace CareerCloud.BusinessLogicLayer
{
	public static class Validator
	{
        public const string PHONE_PATTERN = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
        public const string WEBSITE_PATTERN = @"\.(ca|com|biz)$";
        public const string EMAIL_PATTERN = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

        public static bool IsPhoneNumber(string number)
        {
            if (number != null) return Regex.IsMatch(number, PHONE_PATTERN);
            else return false;
        }
        public static bool IsWebsiteDomainValid(string website)
        {
            if (website != null) return Regex.IsMatch(website, WEBSITE_PATTERN);
            else return false;
        }

        public static bool IsValidEmail(string email)
        {
            if (email != null) return Regex.IsMatch(email, EMAIL_PATTERN, RegexOptions.IgnoreCase);
            else return false;
        }

        
    }
}

