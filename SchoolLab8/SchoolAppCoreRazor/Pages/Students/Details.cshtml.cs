using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.Students;

public class DetailsModel(SchoolContext context) : PageModel
{
    public Student Student { get; private set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var record = await context.Students.AsNoTracking().FirstOrDefaultAsync(record => record.StudentID == id);
        if (record is null)
            return NotFound();
        Student = record;
        return Page();
    }
}
