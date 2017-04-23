using Microsoft.AspNetCore.Mvc;
using MKClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MKBusService.Models
{
    public class DriverMetadata
    {
        public int DriverId { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        [RegularExpression(@"((\d{3}-)|(\(\d{3}\) ))\d{3}-\d{4}")]
        public string HomePhone { get; set; }
        [Required]
        [RegularExpression(@"((\d{3}-)|(\(\d{3}\) ))\d{3}-\d{4}")]
        public string WorkPhone { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        [PostalCodeValidationAttribute]
        public string PostalCode { get; set; }
        [Required]
        [Remote("ProvinceCodeValidation", "MKDrivers")]
        //remote function represents that the validation function resides in the "MKDrivers" controller and its name is "ProvinceCodeValidation"
        public string ProvinceCode { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: dd MMM yyyy}")]
        [DateNotInFutureAttribute]
        public DateTime? DateHired { get; set; }

        public virtual ICollection<Trip> Trip { get; set; }
        public virtual Province ProvinceCodeNavigation { get; set; }
    }

    [ModelMetadataTypeAttribute(typeof(DriverMetadata))]
    public partial class Driver : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield return ValidationResult.Success;
            FirstName = MKValidations.Capitalise(FirstName);
            LastName = MKValidations.Capitalise(LastName);
            FullName = LastName + ", " + FirstName;
            HomePhone = Phoneverification(HomePhone);
            WorkPhone = Phoneverification(WorkPhone);
            ProvinceCode = ProvinceCode.ToUpper();
            PostalCode = PostalCode.ToUpper();
            if (PostalCode.Length == 6)
            {
                PostalCode = PostalCode.Insert(3, " ");
            }
            yield return ValidationResult.Success;
        }
        private string Phoneverification(string Number)
        {
            String copy = "";
            foreach (char digit in Number)
            {
                Regex pattern = new Regex(@"[0-9]");
                if (pattern.IsMatch(digit.ToString()))
                {
                    copy += digit;
                    if (copy.Length == 3 || copy.Length == 7)
                    {
                        copy += "-";
                    }
                }
            }
            return copy;
        }
    }
}
