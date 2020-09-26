using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaterialsWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static MaterialsWebApp.Models.SortViewModel;

namespace MaterialsWebApp.Controllers
{
    public class UnitsController : Controller
    {
        private readonly RecordKeepingContext context;
        public UnitsController(RecordKeepingContext _context)
        {
            context = _context;
        }

        // GET: Units
        [ResponseCache(Duration = 252)]
        public async Task<IActionResult> Index(string name, int page = 1, Sort sortOrder = Sort.IdAsc)
        {
            int pageSize = 20;


            List<Unit> items = context.Units.ToList();
            if (!String.IsNullOrEmpty(name))
            {
                items = items.Where(r => r.Description.Contains(name)).ToList();
            }

            switch (sortOrder)
            {
                case Sort.IdAsc:
                    items = items.OrderBy(r => r.UnitId).ToList();
                    break;
                case Sort.IdDesc:
                    items = items.OrderByDescending(r => r.UnitId).ToList();
                    break;
                case Sort.NameAsc:
                    items = items.OrderBy(r => r.Description).ToList();
                    break;
                case Sort.NameDesc:
                    items = items.OrderByDescending(r => r.Description).ToList();
                    break;
                default:
                    break;
            }
            var count = items.Count();
            items = items.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);

            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Units = items,
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(name)
            };

            return View(viewModel);
        }

        public async Task<IActionResult> IncomeSources()
        {
            return View(await context.Units.ToListAsync());
        }

        // GET: Units/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var source = await context.Units
                .FirstOrDefaultAsync(m => m.UnitId == id);
            if (source == null)
            {
                return NotFound();
            }

            return View(source);
        }

        // GET: Units/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Units/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UnitId,Description")] Unit source)
        {
            if (ModelState.IsValid)
            {
                context.Add(source);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(source);
        }

        // GET: ExpenseTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var call = await context.Units.FindAsync(id);
            if (call == null)
            {
                return NotFound();
            }

            return View(call);
        }

        // POST: ExpenseTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UnitId,Description")] Unit source)
        {
            if (id != source.UnitId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(source);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseTypeExists(source.UnitId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(source);
        }

        // GET: ExpenseTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var call = await context.Units
                .FirstOrDefaultAsync(m => m.UnitId == id);
            if (call == null)
            {
                return NotFound();
            }

            return View(call);
        }

        // POST: ExpenseTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var call = await context.Units.FindAsync(id);
            context.Units.Remove(call);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseTypeExists(int id)
        {
            return context.Units.Any(e => e.UnitId == id);
        }
    }
}
