using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.Departments;

public class IndexModel(SchoolContext context) : PageModel
{
    public IReadOnlyList<Department> Department { get; private set; } = [];

    public async Task OnGetAsync()
    {
        Department = await context.Departments.AsNoTracking().OrderBy(record => record.DepartmentID).ToListAsync();
    }
}
