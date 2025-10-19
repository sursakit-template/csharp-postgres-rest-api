using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebAPI.Models
{
    /// <summary>
    /// An API error model.
    /// </summary>
    public class ApiError
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiError"/> class.
        /// </summary>
        public ApiError()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiError"/> class.
        /// </summary>
        /// <param name="modelState">State of the model.</param>
        public ApiError(ModelStateDictionary modelState)
        {
            IEnumerable<string> errors = modelState.Keys
                .SelectMany(key => modelState[key]?.Errors.Select(x => x.ErrorMessage) ?? Enumerable.Empty<string>());
            Message = string.Join(Environment.NewLine, errors);
        }
    }
}
