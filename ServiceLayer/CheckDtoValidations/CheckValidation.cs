using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ServiceLayer.CheckDtoValidations
{
    public class CheckValidation
    {
        public static bool CheckObjectIsValid<T>(T obj)
        {
            return Validator.TryValidateObject(obj, new System.ComponentModel.DataAnnotations.ValidationContext(obj),
                new List<ValidationResult>());
        }
    }
}
