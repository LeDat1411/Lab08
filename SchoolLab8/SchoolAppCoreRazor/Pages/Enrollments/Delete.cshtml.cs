using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.Enrollments;

public class DeleteModel(SchoolContext context) : PageModel
{
    public Enrollment Enrollment { get; private set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var record = await context.Enrollments.AsNoTracking().Include(record => record.Course).Include(record => record.Student).FirstOrDefaultAsync(record => record.EnrollmentID == id);
        if (record is null)
            return NotFound();
        Enrollment = record;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var record = await context.Enrollments.FindAsync(id);
        if (record is null)
            return NotFound();

        context.Enrollments.Remove(record);
        await context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }
}
