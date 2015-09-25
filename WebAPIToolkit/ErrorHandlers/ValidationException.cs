using System;
using System.Linq;
using System.Web.Http.ModelBinding;
using WebAPIToolkit.Models;

namespace WebAPIToolkit.ErrorHandlers
{
    public class ValidationException : Exception
    {
        public ValidationException(ValidationError errorsDto)
        {
            this.ErrorsDto = errorsDto;
        }

        public ValidationException(ModelStateDictionary modelState)
        {
            this.ErrorsDto = new ValidationError();

            foreach (var error in modelState)
            {
                string errorValues = error.Value.Errors.Aggregate(string.Empty, (current, d) => current + d.ErrorMessage);
                var key = error.Key;
                if (key.StartsWith("dto.", StringComparison.OrdinalIgnoreCase))
                {
                    key = key.Substring(4);
                }
                if (key.StartsWith("model.", StringComparison.OrdinalIgnoreCase))
                {
                    key = key.Substring(6);
                }

                this.ErrorsDto.InvalidInputs.Add(key, errorValues);
            }
        }

        public ValidationException(string field, string message)
        {
            this.ErrorsDto = new ValidationError();
            this.ErrorsDto.InvalidInputs.Add(field, message);
        }

        public ValidationError ErrorsDto { get; private set; }

        public string GetErrorMessage()
        {
            var msg = string.Empty;
            if (ErrorsDto != null && ErrorsDto.InvalidInputs.Count > 0)
            {
                foreach (var invalidInput in ErrorsDto.InvalidInputs)
                {
                    if (msg.Length > 0)
                        msg += " ";
                    msg += $"Field {invalidInput.Key} is not valid : {invalidInput.Value}.";
                }
            }

            return msg;
        }
    }
}
