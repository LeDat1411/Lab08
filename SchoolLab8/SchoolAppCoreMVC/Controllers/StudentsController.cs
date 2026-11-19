using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreMVC.Models;

namespace SchoolAppCoreMVC.Controllers;

public class StudentsController(SchoolContext context) : Controller
{
    public async Task<IActionResult> Index()
    {
        return View(await context.Students.AsNoTracking().OrderBy(record => record.StudentID).ToListAsync());
    }

    public async Task<IActionResult> Details(int id)
    {
        var record = await context.Students.AsNoTracking().FirstOrDefaultAsync(record => record.StudentID == id);
        if (record is null)
            return NotFound();
        return View(record);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("LastName, FirstName, EnrollmentDate")] Student record)
    {
        if (!ModelState.IsValid)
        {
            return View(record);
        }

        context.Students.Add(record);
        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(string.Empty, "The record could not be saved. Check the values and try again.");
            return View(record);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var record = await context.Students.AsNoTracking().FirstOrDefaultAsync(record => record.StudentID == id);
        if (record is null)
            return NotFound();
        return View(record);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("StudentID, LastName, FirstName, EnrollmentDate")] Student record)
    {
        if (id != record.StudentID)
            return NotFound();

        if (!ModelState.IsValid)
        {
            return View(record);
        }

        context.Students.Update(record);
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
            return View(record);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var record = await context.Students.AsNoTracking().FirstOrDefaultAsync(record => record.StudentID == id);
        if (record is null)
            return NotFound();
        return View(record);
    }

    [HttpPost, ActionName(nameof(Delete))]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var record = await context.Students.FindAsync(id);
        if (record is null)
            return NotFound();

        context.Students.Remove(record);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
