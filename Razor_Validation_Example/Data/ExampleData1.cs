using System.ComponentModel.DataAnnotations;

namespace Razor_Validation_Example.Data
{
    public class ExampleData1
    {
        [Required]//DOTNET auto client side.
        [StringLength(5, MinimumLength = 3)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [Url]
        [Display(Name = "URL to Validate")]
        public string MyURL { get; set; }

        [Required]
        [Range(-5.9, 5.9)]
        [Display(Name = "Range Validation")]
        public decimal MyRange { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Validation")]
        public string MyEmail { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string MyPhone { get; set; }

    }
}
