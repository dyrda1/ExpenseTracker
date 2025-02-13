using ExpenseTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Web.Controllers;

public class TransactionController : Controller
{
    private readonly ApplicationDbContext _context;

    public TransactionController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Transaction
    public async Task<IActionResult> Index()
    {
        var applicationDbContext = _context.Transactions.Include(t => t.Category);
        return View(await applicationDbContext.ToListAsync());
    }

    // GET: Transaction/AddOrEdit
    public IActionResult AddOrEdit(int id = 0)
    {
        PopulateCategories();
        if (id == 0)
            return View(new Transaction());
        return View(_context.Transactions.Find(id));
    }

    // POST: Transaction/AddOrEdit
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddOrEdit(
        [Bind("TransactionId,CategoryId,Amount,Note,Date")] Transaction transaction)
    {
        if (ModelState.IsValid)
        {
            if (transaction.TransactionId == 0)
                _context.Add(transaction);
            else
                _context.Update(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        PopulateCategories();
        return View(transaction);
    }

    // POST: Transaction/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction != null) _context.Transactions.Remove(transaction);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }


    [NonAction]
    public void PopulateCategories()
    {
        var categoryCollection = _context.Categories.ToList();
        var defaultCategory = new Category { CategoryId = 0, Title = "Choose a Category" };
        categoryCollection.Insert(0, defaultCategory);
        ViewBag.Categories = categoryCollection;
    }
}