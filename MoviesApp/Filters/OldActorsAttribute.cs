﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApp.Filters
{
    public class OldActorsAttribute: ValidationAttribute
    {
        public OldActorsAttribute(int year)
        {
            Year = year;
        }

        public int Year { get; }

        public string GetErrorMessage() =>
            $"Actor's age should be not more than 99.";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var birthYear = ((DateTime)value).Year;

            if (birthYear < Year)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}
