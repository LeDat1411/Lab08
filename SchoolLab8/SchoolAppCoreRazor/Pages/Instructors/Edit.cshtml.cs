using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.Instructors;

public class EditModel(SchoolContext context) : PageModel
{
    public Instructor Instructor { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var record = await context.Instructors.AsNoTracking().FirstOrDefaultAsync(record => record.InstructorID == id);
        if (record is null)
            return NotFound();
        Instructor = record;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id, [Bind("InstructorID, LastName, FirstName, HireDate", Prefix = "Instructor")] Instructor record)
    {
        Instructor = record;
        if (id != record.InstructorID)
            return NotFound();

        if (!ModelState.IsValid)
        {
            return Page();
        }

        context.Instructors.Update(record);
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
