using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreMVC.Models;

namespace SchoolAppCoreMVC.Controllers;

public class DepartmentsController(SchoolContext context) : Controller
{
    public async Task<IActionResult> Index()
    {
        return View(await context.Departments.AsNoTracking().OrderBy(record => record.DepartmentID).ToListAsync());
    }

    public async Task<IActionResult> Details(int id)
    {
        var record = await context.Departments.AsNoTracking().FirstOrDefaultAsync(record => record.DepartmentID == id);
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
    public async Task<IActionResult> Create([Bind("DepartmentName, Budget, StartDate")] Department record)
    {
        if (!ModelState.IsValid)
        {
            return View(record);
        }

        context.Departments.Add(record);
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
        var record = await context.Departments.AsNoTracking().FirstOrDefaultAsync(record => record.DepartmentID == id);
        if (record is null)
            return NotFound();
        return View(record);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("DepartmentID, DepartmentName, Budget, StartDate")] Department record)
    {
        if (id != record.DepartmentID)
            return NotFound();

        if (!ModelState.IsValid)
        {
            return View(record);
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
            return View(record);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var record = await context.Departments.AsNoTracking().FirstOrDefaultAsync(record => record.DepartmentID == id);
        if (record is null)
            return NotFound();
        return View(record);
    }

    [HttpPost, ActionName(nameof(Delete))]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var record = await context.Departments.FindAsync(id);
        if (record is null)
            return NotFound();

        context.Departments.Remove(record);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
