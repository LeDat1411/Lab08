using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.Enrollments;

public class IndexModel(SchoolContext context) : PageModel
{
    public IReadOnlyList<Enrollment> Enrollment { get; private set; } = [];

    public async Task OnGetAsync()
    {
        Enrollment = await context.Enrollments.AsNoTracking().Include(record => record.Course).Include(record => record.Student).OrderBy(record => record.EnrollmentID).ToListAsync();
    }
}
