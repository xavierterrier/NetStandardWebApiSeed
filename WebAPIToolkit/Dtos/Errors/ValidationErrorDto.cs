using System.Collections.Generic;

namespace WebAPIToolkit.Dtos.Errors
{
    /// <summary>
    /// Represent a BadRequest error
    /// </summary>
    public class ValidationErrorDto
    {
        /// <summary>
        /// The default constructor
        /// </summary>
        public ValidationErrorDto()
        {
            this.FieldErrors = new List<FieldErrorDto>();
        }

        public List<FieldErrorDto> FieldErrors { get; set; }

        /// <summary>
        /// Has errors ?
        /// </summary>
        /// <returns></returns>
        public bool HasErrors()
        {
            return (this.FieldErrors != null && this.FieldErrors.Count > 0);
        }

    }
}
