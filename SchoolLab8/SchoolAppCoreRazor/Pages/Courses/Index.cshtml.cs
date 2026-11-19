using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.Courses;

public class IndexModel(SchoolContext context) : PageModel
{
    public IReadOnlyList<Course> Course { get; private set; } = [];

    public async Task OnGetAsync()
    {
        Course = await context.Courses.AsNoTracking().Include(record => record.Department).OrderBy(record => record.CourseID).ToListAsync();
    }
}
