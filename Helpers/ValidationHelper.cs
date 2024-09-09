using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MedsoftTask1.Helpers
{
    internal class ValidationHelper
    {
        public static bool IsPhoneNumberValid(string phone)
        {
            return Regex.IsMatch(phone, @"^5\d{8}$");
        }

        public static bool AreRequiredFieldsFilled(params string[] fields)
        {
            return fields.All(field => !string.IsNullOrWhiteSpace(field));
        }
    }
}
