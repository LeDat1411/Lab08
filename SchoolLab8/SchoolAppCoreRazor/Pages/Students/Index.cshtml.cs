using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.Students;

public class IndexModel(SchoolContext context) : PageModel
{
    public IReadOnlyList<Student> Student { get; private set; } = [];

    public async Task OnGetAsync()
    {
        Student = await context.Students.AsNoTracking().OrderBy(record => record.StudentID).ToListAsync();
    }
}
