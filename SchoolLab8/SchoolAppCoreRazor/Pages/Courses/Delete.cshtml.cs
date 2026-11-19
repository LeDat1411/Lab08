using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.Courses;

public class DeleteModel(SchoolContext context) : PageModel
{
    public Course Course { get; private set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var record = await context.Courses.AsNoTracking().Include(record => record.Department).FirstOrDefaultAsync(record => record.CourseID == id);
        if (record is null)
            return NotFound();
        Course = record;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var record = await context.Courses.FindAsync(id);
        if (record is null)
            return NotFound();

        context.Courses.Remove(record);
        await context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }
}
