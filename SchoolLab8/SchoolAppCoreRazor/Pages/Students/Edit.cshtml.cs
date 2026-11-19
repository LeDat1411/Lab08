using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.Students;

public class EditModel(SchoolContext context) : PageModel
{
    public Student Student { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var record = await context.Students.AsNoTracking().FirstOrDefaultAsync(record => record.StudentID == id);
        if (record is null)
            return NotFound();
        Student = record;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id, [Bind("StudentID, LastName, FirstName, EnrollmentDate", Prefix = "Student")] Student record)
    {
        Student = record;
        if (id != record.StudentID)
            return NotFound();

        if (!ModelState.IsValid)
        {
            return Page();
        }

        context.Students.Update(record);
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
            return Page();
        }

        return RedirectToPage("./Index");
    }
}
