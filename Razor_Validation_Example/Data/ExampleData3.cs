using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Razor_Validation_Example.Data
{
    public class ExampleData3
    {
        [Display(Name = "My String")]
        [NotBlueAttribute("Server Side: This must be blue!")]
        public string MyString { get; set; }
    }

    public class NotBlueAttribute : ValidationAttribute, IClientValidatable
    {
        private string DefaultErrorMessage = string.Empty;

        public string OtherProperty { get; private set; }

        public NotBlueAttribute(string errorMessage)
        {
            DefaultErrorMessage = errorMessage;
        }

        protected override ValidationResult IsValid(object value,
                             ValidationContext validationContext)
        {
            if (value != null)
            {
                if (!value.ToString().ToLower().Contains("blue"))
                {
                    return new ValidationResult(DefaultErrorMessage);
                    //                    return new ValidationResult(FormatErrorMessage(DefaultErrorMessage));
                }
            }

            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var clientValidationRule = new ModelClientValidationRule()
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "notblue"
            };

           clientValidationRule.ValidationParameters.Add("otherproperty", OtherProperty);

            return new[] { clientValidationRule };
        }
    }
}
