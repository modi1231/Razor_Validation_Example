using System.ComponentModel.DataAnnotations;

namespace Razor_Validation_Example.Data
{
    public class ExampleData2
    {
        [Display(Name = "My String")]
        [NotGreenAttribute("This must be green!")]
        public string MyString { get; set; }
    }

    public class NotGreenAttribute : ValidationAttribute
    {
        private string DefaultErrorMessage = string.Empty;

        public string OtherProperty { get; private set; }

        public NotGreenAttribute(string errorMessage)
        {
            DefaultErrorMessage = errorMessage;
        }

        protected override ValidationResult IsValid(object value,
                             ValidationContext validationContext)
        {
            if (value != null)
            {
                if (!value.ToString().ToLower().Contains("green"))
                {
                    return new ValidationResult(
                      FormatErrorMessage(DefaultErrorMessage));
                }
            }

            return ValidationResult.Success;
        }


    }

}
