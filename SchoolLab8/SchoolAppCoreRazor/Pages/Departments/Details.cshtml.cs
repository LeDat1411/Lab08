using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.Departments;

public class DetailsModel(SchoolContext context) : PageModel
{
    public Department Department { get; private set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var record = await context.Departments.AsNoTracking().FirstOrDefaultAsync(record => record.DepartmentID == id);
        if (record is null)
            return NotFound();
        Department = record;
        return Page();
    }
}
