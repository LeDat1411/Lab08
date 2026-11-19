using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.Departments;

public class CreateModel(SchoolContext context) : PageModel
{
    public Department Department { get; set; } = default!;

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync([Bind("DepartmentName, Budget, StartDate", Prefix = "Department")] Department record)
    {
        Department = record;
        if (!ModelState.IsValid)
        {
            return Page();
        }

        context.Departments.Add(record);
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
