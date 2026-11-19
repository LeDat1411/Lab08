using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.Instructors;

public class CreateModel(SchoolContext context) : PageModel
{
    public Instructor Instructor { get; set; } = default!;

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync([Bind("LastName, FirstName, HireDate", Prefix = "Instructor")] Instructor record)
    {
        Instructor = record;
        if (!ModelState.IsValid)
        {
            return Page();
        }

        context.Instructors.Add(record);
        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(string.Empty, "The record could not be saved. Check the values and try again.");
            return Page();
        }

        return RedirectToPage("./Index");
    }
}
