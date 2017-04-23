using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MKClassLibrary
{
    public class MKValidations : ValidationAttribute
    {
        public static string Capitalise(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("the first letter should be capital !!");
            return input.First().ToString().ToUpper() + input.Substring(1);
        }
    }
}
