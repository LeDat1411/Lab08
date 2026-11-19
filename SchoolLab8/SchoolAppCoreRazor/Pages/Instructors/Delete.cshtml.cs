using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.Instructors;

public class DeleteModel(SchoolContext context) : PageModel
{
    public Instructor Instructor { get; private set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var record = await context.Instructors.AsNoTracking().FirstOrDefaultAsync(record => record.InstructorID == id);
        if (record is null)
            return NotFound();
        Instructor = record;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var record = await context.Instructors.FindAsync(id);
        if (record is null)
            return NotFound();

        context.Instructors.Remove(record);
        await context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }
}
