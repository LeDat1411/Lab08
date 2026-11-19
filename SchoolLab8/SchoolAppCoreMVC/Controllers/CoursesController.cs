using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreMVC.Models;

namespace SchoolAppCoreMVC.Controllers;

public class CoursesController(SchoolContext context) : Controller
{
    public async Task<IActionResult> Index()
    {
        return View(await context.Courses.AsNoTracking().Include(record => record.Department).OrderBy(record => record.CourseID).ToListAsync());
    }

    public async Task<IActionResult> Details(int id)
    {
        var record = await context.Courses.AsNoTracking().Include(record => record.Department).FirstOrDefaultAsync(record => record.CourseID == id);
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
    public async Task<IActionResult> Create([Bind("Title, Credits, DepartmentID")] Course record)
    {
        if (!ModelState.IsValid)
        {
            await LoadOptionsAsync();
            return View(record);
        }

        context.Courses.Add(record);
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
        var record = await context.Courses.AsNoTracking().Include(record => record.Department).FirstOrDefaultAsync(record => record.CourseID == id);
        if (record is null)
            return NotFound();
        await LoadOptionsAsync();
        return View(record);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("CourseID, Title, Credits, DepartmentID")] Course record)
    {
        if (id != record.CourseID)
            return NotFound();

        if (!ModelState.IsValid)
        {
            await LoadOptionsAsync();
            return View(record);
        }

        context.Courses.Update(record);
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
        var record = await context.Courses.AsNoTracking().Include(record => record.Department).FirstOrDefaultAsync(record => record.CourseID == id);
        if (record is null)
            return NotFound();
        return View(record);
    }

    [HttpPost, ActionName(nameof(Delete))]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var record = await context.Courses.FindAsync(id);
        if (record is null)
            return NotFound();

        context.Courses.Remove(record);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadOptionsAsync()
    {
        ViewData["DepartmentIDOptions"] = new SelectList(await context.Departments
            .OrderBy(x => x.DepartmentID)
            .Select(x => new { x.DepartmentID, Label = x.DepartmentID + " - " + x.DepartmentName })
            .ToListAsync(), "DepartmentID", "Label");
    }
}
