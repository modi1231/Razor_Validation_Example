# Razor_Validation_Example
Web Validation in C#
ASP.NET Core 3.0 Razor project to highlight web input validation.

=================
dreamincode.net tutorial backup.

 Posted 18 September 2021 - 03:15 AM 
 
[b][u]Software[/b][/u]
Visual Studio 2019

[b][u]Concepts[/b][/u]
Razor
.NET Core 3.1
Server Side
Client Side
Javascript

[b][u]Github Link:[/b][/u] https://github.com/modi1231/Razor_Validation_Example

This is quick review of the various types of validation Razor provides in the DOTNET Core pages.  I imagine there are many other options and ways to do this, but in my sphere of work these are the top five.  

At the heart of the project is the 'why'.  A webpage can be a simple vehicle for displaying information, or it can allow for user to input information to be stored, emailed, or acted upon.  In the latter part it is a good idea to consider adding in protections of the input so your code knows what to expect and in what format.  Example - if you are making a calculator it would be a good idea to not allow the user to input alpha characters, or if you are expecting an email address for registration information it would be nice to have some checks that it is indeed an email and not just data.

The usual response to invalid data is to put a halt on the process and notify the user the input is not expected.  This is great, but with DOTNET Core pages that may require a trip to the server for validation and then it could trash your model instance.  Definitely a not-so-happy-path.

Validation needs should be determined into your planning phase for your web project, and hammered out before you put keyboard to code.  

On the bright side, even with a basic Javascript file addition to the project, the DOTNET framework picks up the need for input validation on standard properties.  With a bit more complexity, and some DOTNET Core hoodoo, you can create custom validation options that are only limited by your project's needs.

It is a good idea to have a plan on what to expect when a page will do a full postback instead of running off when you are not expecting it.


[b][u]Project Setup[/b][/u]

I created this as an empty Razor project, added the bare minimum middleware, viewimports, viewstart, and shared layout page.

In the _layout I added references to three Javascript libraries - jquery, jquery.validate, and jquery.validate.unobtrusive.  These will mostly be here the entire time until I mention otherwise.

(yes, in your own project you would save the files and reference them locally)

[code]https://cdnjs.cloudflare.com/ajax/libs/jquery/3.4.1/jquery.min.js
https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js
https://cdnjs.cloudflare.com/ajax/libs/jquery-validation-unobtrusive/3.2.11/jquery.validate.unobtrusive.min.js
[/code]

After that was setup it's time to dig into the examples.

[b][u]Example 0[/b][/u]

[img]https://i.imgur.com/tvse7Bj.jpg[/img]

This data model a simple string property.  If the Javascript references are removed from the _layout file, the submit button causes a post back with no issue.

Then if the Javascript references are added back in, the form will not post until values are added in.

This is the important take away - with only some Javascript references the form is on it's way to requiring input.

[code]    public class ExampleData0
    {
        public int MyString { get; set; }
    }[/code]

[code]   public class IndexModel : PageModel
    {
        public ExampleData0 MyExampleData0 { get; set; }
        public IActionResult OnPost()
        {
            System.Diagnostics.Debug.WriteLine($"On Post Hit");

            MESSAGE = "OnPost Happened";

            if (!ModelState.IsValid)
            {
                return Page();
            }
            return RedirectToPage("./Index");
        }

 
    }[/code]

    [code]<form method="post">
        <div>
            <h2>Example 0:</h2>
            <h3>Info:  No validation.  
            <br />If Unobtrusive JS not present, this does a regular post back.  If Unobtrustive JS is there, then this won't post back until a value is entered. </h3>
            @Html.LabelFor(x => Model.MyExampleData0.MyString)
            <input type="text" asp-for="@Model.MyExampleData0.MyString" /><br />
            <br />
            <input type="submit" id="Submit">

        </div>
    </form>[/code]

[b][u]Example 1[/b][/u]

[img]https://i.imgur.com/PZcHnPW.jpg[/img]

This data model goes more into the provided .NET Core validation.  There are a number of options highlighted, but many more you can read in the MSDN documentation.  

The models would have specific data annotations attached, and the HTML side only needs an 'asp-validation-for'.

The bulk of these validation options take the guesswork out of common needs.

Example you can use the data annotation to specify the minimum and maximum length of a string, or basic URL validation in another.  Ranges can be decimals to integers, and even complex email validation is just a simple statement away.

The more complex option is the phone number which, as far as I can tell, requires a bit of REGEX knowledge to make it work.

    [code]public class ExampleData1
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

    }[/code]

 [code]<form method="post">
        <div>
            <h2>Example 1:</h2>
            <h3>Info:  Basic validation provided with DOTNET.</h3>
            @Html.LabelFor(x => Model.MyExampleData1.UserName)
            <input type="text" asp-for="@Model.MyExampleData1.UserName" /><br />
            <span asp-validation-for="@Model.MyExampleData1.UserName"></span>
            <br />
            <br />
            @Html.LabelFor(x => Model.MyExampleData1.MyURL)
            <input type="text" asp-for="@Model.MyExampleData1.MyURL" /><br />
            <span asp-validation-for="@Model.MyExampleData1.MyURL"></span>
            <br />
            <br />
            @Html.LabelFor(x => Model.MyExampleData1.MyRange)
            <input type="text" asp-for="@Model.MyExampleData1.MyRange" /><br />
            <span asp-validation-for="@Model.MyExampleData1.MyRange"></span>
            <br />
            <br />
            @Html.LabelFor(x => Model.MyExampleData1.MyEmail)
            <input type="text" asp-for="@Model.MyExampleData1.MyEmail" /><br />
            <span asp-validation-for="@Model.MyExampleData1.MyEmail"></span>
            <br />
            <br />
            @Html.LabelFor(x => Model.MyExampleData1.MyPhone)
            <input type="text" asp-for="@Model.MyExampleData1.MyPhone" /><br />
            <span asp-validation-for="@Model.MyExampleData1.MyPhone"></span>
            <br />

            <input type="submit" id="Submit">

        </div>
    </form>[/code]


[b][u]Example 2[/b][/u]

[img]https://i.imgur.com/kVPSDPR.jpg[/img]

This data model delves into the custom validation.  It requires the property value to be the string 'green', ignores character casing, and provides 

Be mindful this does trigger a full post back so models and such will be lost if not reloaded.

Create the public string in the model, and then add a new class that inherits from 'ValidationAttribute'.

https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.validationattribute?view=net-5.0

[code]        public class NotGreenAttribute : ValidationAttribute[/code]

In side I typically put a string holder for the custom error message.

[code]        private string DefaultErrorMessage = string.Empty;[/code]

The constructor takes in the error string, and sets it.

[code]        public NotGreenAttribute(string errorMessage)
        {
            DefaultErrorMessage = errorMessage;
        }[/code]

The meat of the process is the 'IsValid' override.  Here it takes in the value of the control you are validating and 'ValidationContext' object (which will be important later).

[code]        protected override ValidationResult IsValid(object value,
                             ValidationContext validationContext)
        {[/code]

It's always a good plan to check if the value for the control you are validating is not null especially when esoteric validation like "does it contain 'green'" that may not be a full on show stopper.
[code]            if (value != null)
            {[/code]

For this example it's a simple case of converting the input to a string, to lower, and check for my key word.

[code]                if (!value.ToString().ToLower().Contains("green"))
                {[/code]

Opting to report on the negative, take the custom error message, have .NET format it, and return that error.

[code]                    return new ValidationResult(
                      FormatErrorMessage(DefaultErrorMessage));
                }
            }[/code]

If it passes then report the success.

[code]            return ValidationResult.Success;
        }[/code]

Implementation is a snap, and like any other data annotation.  The attribute name, and then string of text for the custom error!

[code]    public class ExampleData2
    {
        [Display(Name = "My String")]
        [NotGreenAttribute("This must be green!")]
        public string MyString { get; set; }[/code]

On the flip side in the HTML it looks like the prior example.  

[code]            @Html.LabelFor(x => Model.MyExampleData2.MyString)
            <input type="text" asp-for="@Model.MyExampleData2.MyString" value="red" /><br />
            <span asp-validation-for="@Model.MyExampleData2.MyString" ></span>[/code]


[b][u]Example 3[/b][/u]

[img]https://i.imgur.com/04zHEx7.jpg[/img]

This example takes the Example 2 custom validator example a step further and stops the post back using some ganked Javascript.

Once again, the validation is a model with a single string.  This time the custom validation looks if the string contains the word 'blue' with casing agnostics.

The important change is the new inherited interface - IClientValidatable.  It's all about helping ASP.NET validation to know if a validator should be client side.

https://docs.microsoft.com/en-us/dotnet/api/system.web.mvc.iclientvalidatable?view=aspnet-mvc-5.2

[code]   public class NotBlueAttribute : ValidationAttribute, IClientValidatable
    {[/code]

For the change of pace, I switched up the IsValid method in two places.  First is the check on what word it contains, and the second is the returned error message is no longer being formatted by .NET.

[code]        protected override ValidationResult IsValid(object value,
                             ValidationContext validationContext)
        {
            if (value != null)
            {
                if (!value.ToString().ToLower().Contains("blue"))
                {
                    return new ValidationResult(DefaultErrorMessage);
                }
            }
            return ValidationResult.Success;
        }[/code]

The bigger change is implementing that IClientValidate interface.  For the most part I used an all lower case version of the attribute name (minus 'attribute'), and filled in the properties.  Pretty straight ganked from MSDN.

[code]        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var clientValidationRule = new ModelClientValidationRule()
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "notblue"
            };[/code]

           clientValidationRule.ValidationParameters.Add("otherproperty", OtherProperty);

            return new[] { clientValidationRule };
        }

On the HTML side things change up with a the addition of the Javascript.

The first chunk is it is important to have the 'ValidationType' name from above when adding the validator method.
[code]        <script>
            (function ($) {
                $.validator.addMethod("notblue", function (value, element, params) {[/code]


This next bit will require translating your 'IsValid' into Javascript.  Yup, in to Javascript.  It's a bummer, but mostly not that bad.

[code]                    if (!this.optional(element)) {
                        var otherProp = $('#' + params)
                        return (value.includes("blue"));
                    }
                    return true;
                });[/code]

The last section is just about adding the lower-cased property name to your lower-cased attribute name.  That will be important in the next section.

[code]                $.validator.unobtrusive.adapters.addSingleVal("notblue", "otherproperty");

            }(jQuery));
        </script>[/code]

With that all out of the way, there are a few black magic bits to add to the controls.

The key 'data-val' needs to be set true, and then use the lower-cased attribute name ('data-val-notblue') to set the error string.  Again it's all about splattering that attribute name where it needs to go.

[code]            @Html.LabelFor(x => Model.MyExampleData3.MyString)
            <input type="text" asp-for="@Model.MyExampleData3.MyString" value="red"
                   data-val="true"
                   data-val-notblue="Client Side: This must be blue!"
                   data-val-notblue-otherproperty="foo" />[/code]

Then the validation adds the whole 'data-valmsg-replace' to make sure that custom error message makes it through.

[code]            <span asp-validation-for="@Model.MyExampleData3.MyString" data-valmsg-replace="true"></span>[/code]


[b][u]Example 4[/b][/u]

[img]https://i.imgur.com/wtwu3Ay.jpg[/img]

The final example shows a pretty complex (but often needed) validation - comparing two properties.  A good example of this is a password reset where the old and new password cannot equal each other.

The validation will straddle server side and client side.

The model has two strings - value1 and value2.  This attribute's name is "NotTheSameAttribute", and each data attribute will have the string name of the other string property.

[code]        [NotTheSameAttribute("Value2", "Server Side: Cannnot be the same value!")]
        [Display(Name = "Value 1")]
        public string Value1 { get; set; }

        [NotTheSameAttribute("Value1", "Server Side: Cannnot be the same value!")]
        [Display(Name = "Value 2")]
        public string Value2 { get; set; }[/code]


The attribute inherits from ValidationAttribute and IClientValidatable like the prior example.  The important change is the 'OtherProperty' string is being utilized to hold that comparison property's name.

 [code]   public class NotTheSameAttribute : ValidationAttribute, IClientValidatable
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
        }[/code]


The IsValid result does some more black magic using the validationContext to get the actual property then dive into to getting the value from that other property.

[code]        protected override ValidationResult IsValid(object value,
                             ValidationContext validationContext)
        {
            if (value != null)
            {
                var otherProperty = validationContext.ObjectInstance.GetType()
                                   .GetProperty(OtherProperty);

                var otherPropertyValue = otherProperty
                    .GetValue(validationContext.ObjectInstance, null);[/code]

On the back side of that, is a normal comparison of values and figuring out what to return.  In this case straight up string comparison to lower cased strings.

[code]                if (otherPropertyValue != null && value.ToString().ToLower().Equals(otherPropertyValue.ToString().ToLower()))
                {
                    return new ValidationResult(
                      FormatErrorMessage(validationContext.DisplayName));
                }
            }

            return ValidationResult.Success;
        }[/code]


Much like the third example, the 'GetClientValidationRules' only needs to be copied and the validationtype to be the attribute name but all lower case, and the 'otherproperty' name added to the validation parameters.

[code]        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, System.Web.Mvc.ControllerContext context)
        {
            var clientValidationRule = new ModelClientValidationRule()
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "notthesame"
            };

            clientValidationRule.ValidationParameters.Add("otherproperty", OtherProperty);

            return new[] { clientValidationRule };
        }[/code]

The HTML side also looks like the previous example, but with a few tweaks.

First the Javascript is different to the point where the condition checks if the other control's value is the same.  Per usual, it has the lowercase attribute name.

[code]        <script>
            (function ($) {
                $.validator.addMethod("notthesame", function (value, element, params) {
                    if (!this.optional(element)) {
                        var otherProp = $('#' + params)
                        return (otherProp.val() != value);
                    }
                    return true;
                });
                $.validator.unobtrusive.adapters.addSingleVal("notthesame", "otherproperty");

            }(jQuery));
        </script>[/code]

The interesting change here is the 'data-val-notthesame-otherproperty' specifies the OTHER control's name.  This maybe a bit tricky to determine what the compiled name is, but the quick ways it to run the page, hit F12 for the dev tools, and find the control.

[code]             <h3>More info.  Client Side validation comparison between properties</h3>
            @Html.LabelFor(x => Model.MyExampleData4.Value1)
            <input type="text" asp-for="@Model.MyExampleData4.Value1" value="orange"
                   data-val="true"
                   data-val-notthesame="Client Side: These cannot match!"
                   data-val-notthesame-otherproperty="MyExampleData4_Value2" />
            <br />
            <span asp-validation-for="@Model.MyExampleData4.Value1" data-valmsg-replace="true"></span>
            <br />
            @Html.LabelFor(x => Model.MyExampleData4.Value2)
            <input type="text" asp-for="@Model.MyExampleData4.Value2" value="orange"
                   data-val="true"
                   data-val-notthesame="Client Side: These cannot match!"
                   data-val-notthesame-otherproperty="MyExampleData4_Value1" />
            <br />
            <span asp-validation-for="@Model.MyExampleData4.Value2" data-valmsg-replace="true"></span>
            <br />[/code]


That should cover it all.  A fairly collected view of different ways to implement validation ranging from the simple to the super complex on both sides of the render.

[u]Some extra reading:[/u]
https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-5.0
https://docs.microsoft.com/en-us/aspnet/core/tutorials/razor-pages/validation?view=aspnetcore-5.0&tabs=visual-studio
