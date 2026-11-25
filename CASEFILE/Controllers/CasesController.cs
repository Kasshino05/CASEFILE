
using CASEFILE.Data;
using CASEFILE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CASEFILE.Controllers;

public class CasesController : Controller
{
    private readonly AppDbContext _context;

    public CasesController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var cases = await _context.Cases
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();

        return View(cases);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Case newCase)
    {
        bool referenceExists = await _context.Cases
            .AnyAsync(c => c.ReferenceNumber == newCase.ReferenceNumber);

        if (referenceExists)
        {
            ModelState.AddModelError(
                nameof(newCase.ReferenceNumber),
                "This reference number already exists."
            );
        }

        if (!ModelState.IsValid)
        {
            return View(newCase);
        }

        newCase.CreatedAt = DateTime.UtcNow;

        _context.Cases.Add(newCase);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Details), new { id = newCase.Id });
    }

    public async Task<IActionResult> Details(int id)
    {
        var caseItem = await _context.Cases
            .FirstOrDefaultAsync(c => c.Id == id);

        if (caseItem == null)
        {
            return NotFound();
        }

        return View(caseItem);
    }
}
