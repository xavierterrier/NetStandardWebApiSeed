using System.Collections.Generic;

namespace WebAPIToolkit.Models
{
    public class ValidationError
    {
        public ValidationError()
        {
            this.InvalidInputs = new Dictionary<string, string>();
        }

        public Dictionary<string, string> InvalidInputs { get; set; }

        public bool HasErrors()
        {
            return (this.InvalidInputs != null && this.InvalidInputs.Count > 0);
        }

        /// <summary>
        /// Merge with another ValidationError
        /// </summary>
        /// <param name = "masterKey">Master Key for InvalidPuts dictionnary</param>
        /// <param name="errors">Errors.</param>
        public void Merge(string masterKey, ValidationError errors)
        {
            foreach (var error in errors.InvalidInputs)
            {
                InvalidInputs.Add($"{masterKey}.{error.Key}", error.Value);
            }
        }

    }
}
