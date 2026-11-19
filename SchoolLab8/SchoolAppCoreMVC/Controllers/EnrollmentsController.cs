using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreMVC.Models;

namespace SchoolAppCoreMVC.Controllers;

public class EnrollmentsController(SchoolContext context) : Controller
{
    public async Task<IActionResult> Index()
    {
        return View(await context.Enrollments.AsNoTracking().Include(record => record.Course).Include(record => record.Student).OrderBy(record => record.EnrollmentID).ToListAsync());
    }

    public async Task<IActionResult> Details(int id)
    {
        var record = await context.Enrollments.AsNoTracking().Include(record => record.Course).Include(record => record.Student).FirstOrDefaultAsync(record => record.EnrollmentID == id);
        if (record is null)
            return NotFound();
        return View(record);
    }

    public async Task<IActionResult> Create()
    {
        await LoadOptionsAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("CourseID, StudentID, Grade")] Enrollment record)
    {
        if (!ModelState.IsValid)
        {
            await LoadOptionsAsync();
            return View(record);
        }

        context.Enrollments.Add(record);
        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(string.Empty, "The record could not be saved. Check the values and try again.");
            await LoadOptionsAsync();
            return View(record);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var record = await context.Enrollments.AsNoTracking().Include(record => record.Course).Include(record => record.Student).FirstOrDefaultAsync(record => record.EnrollmentID == id);
        if (record is null)
            return NotFound();
        await LoadOptionsAsync();
        return View(record);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("EnrollmentID, CourseID, StudentID, Grade")] Enrollment record)
    {
        if (id != record.EnrollmentID)
            return NotFound();

        if (!ModelState.IsValid)
        {
            await LoadOptionsAsync();
            return View(record);
        }

        context.Enrollments.Update(record);
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
            await LoadOptionsAsync();
            return View(record);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var record = await context.Enrollments.AsNoTracking().Include(record => record.Course).Include(record => record.Student).FirstOrDefaultAsync(record => record.EnrollmentID == id);
        if (record is null)
            return NotFound();
        return View(record);
    }

    [HttpPost, ActionName(nameof(Delete))]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var record = await context.Enrollments.FindAsync(id);
        if (record is null)
            return NotFound();

        context.Enrollments.Remove(record);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadOptionsAsync()
    {
        ViewData["CourseIDOptions"] = new SelectList(await context.Courses
            .OrderBy(x => x.CourseID)
            .Select(x => new { x.CourseID, Label = x.CourseID + " - " + x.Title })
            .ToListAsync(), "CourseID", "Label");
        ViewData["StudentIDOptions"] = new SelectList(await context.Students
            .OrderBy(x => x.StudentID)
            .Select(x => new { x.StudentID, Label = x.StudentID + " - " + x.FirstName + " " + x.LastName })
            .ToListAsync(), "StudentID", "Label");
    }
}
