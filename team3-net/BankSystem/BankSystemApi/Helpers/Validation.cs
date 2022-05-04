using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BankSystemApi.Helpers
{
    public static class Validation
    {
        public static List<string> Errors { get; set; } = new List<string>();
        public enum Bank { privatbank, monobank, alfabank, bank };
        public static bool isNatural(this string n)
        {
            try
            {
                int k = Int32.Parse(n);
                if (k < 1)
                {
                    Errors.Add("Value cannot be less than 1");
                    return false;
                }
                return true;
            }
            catch
            {
                throw new ArgumentException("Number is not Natural");
            }
        }

        public static bool isWord(string s)
        {
            foreach (char c in s)
            {
                if (!Char.IsLetter(c))
                {
                    Errors.Add("Entered string is not word");
                    return false;
                }
            }
            return true;
        }

        public static bool isDate(string date)
        {
            try
            {
                DateTime dat = DateTime.Parse(date);
                return true;
            }
            catch
            {
                Errors.Add("Invalid date!");
                return false;
            }
        }
        public static bool isCVC(string x)
        {
            if ((!isNatural(x)) || x.Length != 3)
            {
                Errors.Add("Entered value is not CVC");
                return false;
            }
            return true;
        }

        public static bool checkFile(string way)
        {
            if (!File.Exists(way))
            {
                Errors.Add("File does not exists");
                return false;
            }
            return true;
        }

        public static bool checkDateOfExpire(string date, DateTime? date_of_issue)
        {
            if ((!isDate(date)) || (DateTime.Parse(date) < date_of_issue))
            {
                Errors.Add("Wrong date of expire");
                return false;
            }
            return true;
        }

        public static bool isBank(string data)
        {
            bool checker = true;
            foreach (var bank in Enum.GetNames(typeof(Bank)))
            {
                if (data?.ToLower() == bank)
                {
                    checker = false;
                }
            }
            if (checker)
            {
                Errors.Add("It is not bank");
                return false;
            }
            return true;
        }

        public static bool isName(string stroke)
        {
            bool checker = false;
            string[] words = stroke?.Split(" ");
            if (words?.Length != 2)
            {
                checker = true;
            }
            for (int i = 0; i < words?.Length; i++)
            {
                if (!isWord(words[i]))
                {
                    checker = true;
                }
            }
            if (checker)
            {
                Errors.Add("It is not name");
                return false;
            }
            return true;
        }

        public static bool isCardNumber(string stroke)
        {
            bool checker = false;
            string[] numbers = stroke?.Split(" ");
            if (numbers?.Length != 4)
            {
                checker = true;
            }
            for (int i = 0; i < numbers?.Length; i++)
            {
                if ((!isNatural(numbers[i])) || numbers[i]?.Length != 4)
                {
                    checker = true;
                    break;
                }
            }
            if (checker)
            {
                Errors.Add("It is not card number");
                return false;
            }
            return true;
        }
        public static bool isAttribute(object card, string attr)
        {
            var propertyinfo = card.GetType().GetProperties();
            foreach (var item in propertyinfo)
            {
                if (item.Name == attr)
                {
                    return true;
                }
            }
            throw new ArgumentException("Not correct attribute");
        }

        public static bool IsValid()
        {
            if (Errors.Any())
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
