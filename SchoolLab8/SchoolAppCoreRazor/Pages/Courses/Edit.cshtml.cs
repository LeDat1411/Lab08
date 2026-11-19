using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.Courses;

public class EditModel(SchoolContext context) : PageModel
{
    public Course Course { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var record = await context.Courses.AsNoTracking().Include(record => record.Department).FirstOrDefaultAsync(record => record.CourseID == id);
        if (record is null)
            return NotFound();
        await LoadOptionsAsync();
        Course = record;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id, [Bind("CourseID, Title, Credits, DepartmentID", Prefix = "Course")] Course record)
    {
        Course = record;
        if (id != record.CourseID)
            return NotFound();

        if (!ModelState.IsValid)
        {
            await LoadOptionsAsync();
            return Page();
        }

        context.Courses.Update(record);
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
        ViewData["DepartmentIDOptions"] = new SelectList(await context.Departments
            .OrderBy(x => x.DepartmentID)
            .Select(x => new { x.DepartmentID, Label = x.DepartmentID + " - " + x.DepartmentName })
            .ToListAsync(), "DepartmentID", "Label");
    }
}
