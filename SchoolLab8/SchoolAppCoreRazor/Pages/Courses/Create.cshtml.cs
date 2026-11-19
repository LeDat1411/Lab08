using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.Courses;

public class CreateModel(SchoolContext context) : PageModel
{
    public Course Course { get; set; } = default!;

    public async Task<IActionResult> OnGet()
    {
        await LoadOptionsAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync([Bind("Title, Credits, DepartmentID", Prefix = "Course")] Course record)
    {
        Course = record;
        if (!ModelState.IsValid)
        {
            await LoadOptionsAsync();
            return Page();
        }

        context.Courses.Add(record);
        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(string.Empty, "The record could not be saved. Check the values and try again.");
            await LoadOptionsAsync();
            return Page();
        }

        return RedirectToPage("./Index");
    }

    private async Task LoadOptionsAsync()
    {
        ViewData["DepartmentIDOptions"] = new SelectList(await context.Departments
            .OrderBy(x => x.DepartmentID)
            .Select(x => new { x.DepartmentID, Label = x.DepartmentID + " - " + x.DepartmentName })
            .ToListAsync(), "DepartmentID", "Label");
    }
}
