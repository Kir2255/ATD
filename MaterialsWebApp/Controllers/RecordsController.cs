using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaterialsWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static MaterialsWebApp.Models.SortViewModel;

namespace MaterialsWebApp.Controllers
{
    public class RecordsController : Controller
    {
        private readonly RecordKeepingContext context;
        public RecordsController(RecordKeepingContext _context)
        {
            context = _context;
        }

        // GET: Units
        [ResponseCache(Duration = 252)]
        public async Task<IActionResult> Index(string name, int page = 1, Sort sortOrder = Sort.IdAsc)
        {
            int pageSize = 20;


            List<IncomeExpenseBook> items = await context.IncomeExpenseBook.Include(r => r.Component.Detail)
                .Include(r => r.Component.Material)
                .Include(r => r.Component.Unit)
                .ToListAsync();
            if (!String.IsNullOrEmpty(name)) items = items.Where(r => r.Component.Material.Name.Contains(name)).ToList();

            switch (sortOrder)
            {
                case Sort.IdAsc:
                    items = items.OrderBy(r => r.RecordId).ToList();
                    break;
                case Sort.IdDesc:
                    items = items.OrderByDescending(r => r.RecordId).ToList();
                    break;
                case Sort.CodeAsc:
                    items = items.OrderBy(r => r.Component.Detail.Code).ToList();
                    break;
                case Sort.CodeDesc:
                    items = items.OrderByDescending(r => r.Component.Detail.Code).ToList();
                    break;
                case Sort.MCodeAsc:
                    items = items.OrderBy(r => r.Component.Material.Code).ToList();
                    break;
                case Sort.MCodeDesc:
                    items = items.OrderByDescending(r => r.Component.Material.Code).ToList();
                    break;
                case Sort.NameAsc:
                    items = items.OrderBy(r => r.Component.Material.Name).ToList();
                    break;
                case Sort.NameDesc:
                    items = items.OrderByDescending(r => r.Component.Material.Name).ToList();
                    break;
                case Sort.UnitAsc:
                    items = items.OrderBy(r => r.Component.Unit.Description).ToList();
                    break;
                case Sort.UnitDesc:
                    items = items.OrderByDescending(r => r.Component.Unit.Description).ToList();
                    break;
                case Sort.IncomeAsc:
                    items = items.OrderBy(r => r.Income).ToList();
                    break;
                case Sort.IncomeDesc:
                    items = items.OrderByDescending(r => r.Income).ToList();
                    break;
                case Sort.ExpenseAsc:
                    items = items.OrderBy(r => r.Expense).ToList();
                    break;
                case Sort.ExpenseDesc:
                    items = items.OrderByDescending(r => r.Expense).ToList();
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
                IncomeExpenseBooks = items,
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(name)
            };

            return View(viewModel);
        }

        public async Task<IActionResult> IncomeSources()
        {
            return View(await context.IncomeExpenseBook.ToListAsync());
        }

        // GET: Units/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var source = await context.IncomeExpenseBook.Include(r => r.Component.Detail).Include(r => r.Component.Material).Include(r => r.Component.Unit)
                .FirstOrDefaultAsync(m => m.RecordId == id);
            if (source == null)
            {
                return NotFound();
            }

            return View(source);
        }

        // GET: Units/Create
        public IActionResult Create()
        {
            //ViewData["ComponentId"] = new SelectList(context.Components.Include(c => c.Detail).Include(c => c.Material), "ComponentId", "Material.Name");
            ViewData["ComponentId"] = new SelectList(context.Components, "ComponentId", "ComponentId");
            return View();
        }

        // POST: Units/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecordId,ComponentId,Income,Expense")] IncomeExpenseBook source)
        {
            if (ModelState.IsValid)
            {
                context.Add(source);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            //ViewData["ComponentId"] = new SelectList(context.Components.Include(c => c.Detail).Include(c => c.Material), "ComponentId", "Material.Name", source.RecordId);
            ViewData["ComponentId"] = new SelectList(context.Components, "ComponentId", "ComponentId", source.RecordId);
            return View(source);
        }

        // GET: ExpenseTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var call = await context.IncomeExpenseBook.FindAsync(id);
            if (call == null)
            {
                return NotFound();
            }

            ViewData["ComponentId"] = new SelectList(context.Components, "ComponentId", "ComponentId", call.RecordId);
            return View(call);
        }

        // POST: ExpenseTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RecordId,ComponentId,Income,Expense")] IncomeExpenseBook source)
        {
            if (id != source.RecordId)
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
                    if (!RecordExists(source.RecordId))
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

            ViewData["ComponentId"] = new SelectList(context.Components, "ComponentId", "ComponentId", source.RecordId);
            return View(source);
        }

        // GET: ExpenseTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var call = await context.IncomeExpenseBook.Include(r => r.Component.Detail).Include(r => r.Component.Material).Include(r => r.Component.Unit)
                .FirstOrDefaultAsync(m => m.RecordId == id);
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
            var call = await context.IncomeExpenseBook.FindAsync(id);
            context.IncomeExpenseBook.Remove(call);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecordExists(int id)
        {
            return context.IncomeExpenseBook.Any(e => e.RecordId == id);
        }
    }
}
