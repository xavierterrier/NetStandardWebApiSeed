using System;
using System.Linq;
using System.Web.Http.ModelBinding;
using WebAPIToolkit.Dtos.Errors;

namespace WebAPIToolkit.Common.ErrorHandlers
{
    /// <summary>
    /// Represent a Validation Exception (Bad request)
    /// By raising this in a Controller call, a BadRequest (HTTP error 400) is returned to the user with a ValidationErrorDto
    /// </summary>
    public class BadRequestException : Exception
    {
        public BadRequestException(ValidationErrorDto errorsDto)
        {
            this.ErrorsDto = errorsDto;
        }

        public BadRequestException(ModelStateDictionary modelState)
        {
            this.ErrorsDto = new ValidationErrorDto();

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

                this.ErrorsDto.FieldErrors.Add(new FieldErrorDto(key, errorValues, null));
            }
        }

        public BadRequestException(string field, string message)
        {
            this.ErrorsDto = new ValidationErrorDto();
            this.ErrorsDto.FieldErrors.Add(new FieldErrorDto(field, message, null));
        }

        public ValidationErrorDto ErrorsDto { get; private set; }

        public string GetErrorMessage()
        {
            var msg = string.Empty;
            if (ErrorsDto != null && ErrorsDto.FieldErrors.Count > 0)
            {
                foreach (var invalidInput in ErrorsDto.FieldErrors)
                {
                    if (msg.Length > 0)
                        msg += " ";
                    msg += $"Field {invalidInput.Path} is not valid : {invalidInput.Message}.";
                }
            }

            return msg;
        }
    }
}
