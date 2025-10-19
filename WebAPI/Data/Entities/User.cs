using System.ComponentModel.DataAnnotations;

namespace WebAPI.Data.Entities
{
    /// <summary>
    /// The User entity.
    /// </summary>
    public class User : IdentifiableEntity
    {
        /// <summary>
        /// Gets or sets the user's full name.
        /// </summary>
        /// <value>
        /// The user's full name.
        /// </value>
        [Required]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        /// <value>
        /// The user's email address.
        /// </value>
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        /// <summary>
        /// Gets or sets the user's phone number.
        /// </summary>
        /// <value>
        /// The user's phone number.
        /// </value>
        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = null!;
    }
}
