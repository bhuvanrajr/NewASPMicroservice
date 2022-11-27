using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        Dictionary<string, string[]> Errors;
        public ValidationException() : base("One or more validations failed")
        {
            Errors = new Dictionary<string, string[]>();
        }
        public ValidationException(IEnumerable<ValidationFailure> validationFailures)
        {
            Errors = validationFailures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(f => f.Key, f => f.ToArray());
        }
    }
}
