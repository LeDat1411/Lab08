using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.Enrollments;

public class CreateModel(SchoolContext context) : PageModel
{
    public Enrollment Enrollment { get; set; } = default!;

    public async Task<IActionResult> OnGet()
    {
        await LoadOptionsAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync([Bind("CourseID, StudentID, Grade", Prefix = "Enrollment")] Enrollment record)
    {
        Enrollment = record;
        if (!ModelState.IsValid)
        {
            await LoadOptionsAsync();
            return Page();
        }

        context.Enrollments.Add(record);
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
        ViewData["CourseIDOptions"] = new SelectList(await context.Courses
            .OrderBy(x => x.CourseID)
            .Select(x => new { x.CourseID, Label = x.CourseID + " - " + x.Title })
            .ToListAsync(), "CourseID", "Label");
        ViewData["StudentIDOptions"] = new SelectList(await context.Students
            .OrderBy(x => x.StudentID)
            .Select(x => new { x.StudentID, Label = x.StudentID + " - " + x.FirstName + " " + x.LastName })
            .ToListAsync(), "StudentID", "Label");
    }
}
