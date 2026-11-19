using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.Departments;

public class EditModel(SchoolContext context) : PageModel
{
    public Department Department { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var record = await context.Departments.AsNoTracking().FirstOrDefaultAsync(record => record.DepartmentID == id);
        if (record is null)
            return NotFound();
        Department = record;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id, [Bind("DepartmentID, DepartmentName, Budget, StartDate", Prefix = "Department")] Department record)
    {
        Department = record;
        if (id != record.DepartmentID)
            return NotFound();

        if (!ModelState.IsValid)
        {
            return Page();
        }

        context.Departments.Update(record);
        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return NotFound();
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(string.Empty, "The record could not be saved. Check the values and try again.");
            return Page();
        }

        return RedirectToPage("./Index");
    }
}
