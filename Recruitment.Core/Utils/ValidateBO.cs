using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace CAESDO.Recruitment.Core.Utils
{
    public static class ValidateBO<T>
    {
        public static bool isValid(T obj)
        {
            return Validation.Validate<T>(obj).IsValid;
        }

        public static ValidationResults GetValidationResults(T obj)
        {
            return Validation.Validate<T>(obj);
        }
    }
}
