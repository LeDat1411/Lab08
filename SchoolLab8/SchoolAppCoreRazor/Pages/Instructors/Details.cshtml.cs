using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.Instructors;

public class DetailsModel(SchoolContext context) : PageModel
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
}
