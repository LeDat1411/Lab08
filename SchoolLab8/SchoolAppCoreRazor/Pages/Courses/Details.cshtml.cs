using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.Courses;

public class DetailsModel(SchoolContext context) : PageModel
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
}
