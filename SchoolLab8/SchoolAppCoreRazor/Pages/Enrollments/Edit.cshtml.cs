using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.Enrollments;

public class EditModel(SchoolContext context) : PageModel
{
    public Enrollment Enrollment { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var record = await context.Enrollments.AsNoTracking().Include(record => record.Course).Include(record => record.Student).FirstOrDefaultAsync(record => record.EnrollmentID == id);
        if (record is null)
            return NotFound();
        await LoadOptionsAsync();
        Enrollment = record;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id, [Bind("EnrollmentID, CourseID, StudentID, Grade", Prefix = "Enrollment")] Enrollment record)
    {
        Enrollment = record;
        if (id != record.EnrollmentID)
            return NotFound();

        if (!ModelState.IsValid)
        {
            await LoadOptionsAsync();
            return Page();
        }

        context.Enrollments.Update(record);
        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return NotFound();
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
