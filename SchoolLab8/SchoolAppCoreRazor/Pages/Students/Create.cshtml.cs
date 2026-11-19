using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.Students;

public class CreateModel(SchoolContext context) : PageModel
{
    public Student Student { get; set; } = default!;

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync([Bind("LastName, FirstName, EnrollmentDate", Prefix = "Student")] Student record)
    {
        Student = record;
        if (!ModelState.IsValid)
        {
            return Page();
        }

        context.Students.Add(record);
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
