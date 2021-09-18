using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Razor_Validation_Example.Data
{
    public class ExampleData4
    {
        //compare two properties.

        [NotTheSameAttribute("Value2", "Server Side: Cannnot be the same value!")]
        [Display(Name = "Value 1")]
        public string Value1 { get; set; }

        [NotTheSameAttribute("Value1", "Server Side: Cannnot be the same value!")]
        [Display(Name = "Value 2")]
        public string Value2 { get; set; }
    }

    public class NotTheSameAttribute : ValidationAttribute, IClientValidatable
    {
        private string DefaultErrorMessage = string.Empty;

        public string OtherProperty { get; private set; }

        public NotTheSameAttribute(string otherProperty, string errorMessage)
        {
            if (string.IsNullOrEmpty(otherProperty))
            {
                throw new ArgumentNullException("otherProperty");
            }

            OtherProperty = otherProperty;

            DefaultErrorMessage = errorMessage;
        }

        protected override ValidationResult IsValid(object value,
                             ValidationContext validationContext)
        {
            if (value != null)
            {
                var otherProperty = validationContext.ObjectInstance.GetType()
                                   .GetProperty(OtherProperty);

                var otherPropertyValue = otherProperty
                    .GetValue(validationContext.ObjectInstance, null);


                if (otherPropertyValue != null && value.ToString().ToLower().Equals(otherPropertyValue.ToString().ToLower()))
                {
                    return new ValidationResult(
                      FormatErrorMessage(validationContext.DisplayName));
                }
            }

            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, System.Web.Mvc.ControllerContext context)
        {
            var clientValidationRule = new ModelClientValidationRule()
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "notthesame"
            };

            clientValidationRule.ValidationParameters.Add("otherproperty", OtherProperty);

            return new[] { clientValidationRule };
        }
    }
}
