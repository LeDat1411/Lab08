using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.Instructors;

public class IndexModel(SchoolContext context) : PageModel
{
    public IReadOnlyList<Instructor> Instructor { get; private set; } = [];

    public async Task OnGetAsync()
    {
        Instructor = await context.Instructors.AsNoTracking().OrderBy(record => record.InstructorID).ToListAsync();
    }
}
