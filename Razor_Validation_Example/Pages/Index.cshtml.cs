using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor_Validation_Example.Data;

namespace Razor_Validation_Example.Pages
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/aspnet/core/tutorials/razor-pages/validation?view=aspnetcore-5.0&tabs=visual-studio
    /// 
    /// </summary>
    public class IndexModel : PageModel
    {
        public ExampleData0 MyExampleData0 { get; set; }

        public ExampleData1 MyExampleData1 { get; set; }

        [BindProperty]
        public ExampleData2 MyExampleData2 { get; set; }

        [BindProperty]
        public ExampleData3 MyExampleData3 { get; set; }


        [BindProperty]
        public ExampleData4 MyExampleData4 { get; set; }

        [TempData]
        public string MESSAGE { get; set; }
        public void OnGet()
        {
        }

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

 
    }
}
