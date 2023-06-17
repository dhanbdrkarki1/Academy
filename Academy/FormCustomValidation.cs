using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

namespace Academy
{
    public class FormCustomValidation
    {
        public static bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim().ToLower();

            // Define the regex pattern for email validation
            string pattern = @"^[a-zA-Z0-9_.]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(pattern);

            if (!regex.IsMatch(trimmedEmail))
            {
               return false;
            }
            else
            {
                return true;
            }
        }



        // password validation
        public static bool Validate_Password(string password)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");
            var isValidated = hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password) && hasSymbols.IsMatch(password);

            if (!hasMinimum8Chars.IsMatch(password))
            {
                return false;
            }
            else if (!isValidated)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}