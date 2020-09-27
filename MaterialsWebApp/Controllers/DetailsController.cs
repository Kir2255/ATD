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
    public class DetailsController : Controller
    {
        private readonly RecordKeepingContext context;
        public DetailsController(RecordKeepingContext _context)
        {
            context = _context;
        }

        // GET: Units
        [ResponseCache(Duration = 252)]
        public async Task<IActionResult> Index(string name, int page = 1, Sort sortOrder = Sort.IdAsc)
        {
            int pageSize = 20;


            List<Detail> items = await context.Details.ToListAsync();
            if (!String.IsNullOrEmpty(name))
            {
                items = items.Where(r => r.Code.ToString().Contains(name)).ToList();
            }

            switch (sortOrder)
            {
                case Sort.IdAsc:
                    items = items.OrderBy(r => r.DetailId).ToList();
                    break;
                case Sort.IdDesc:
                    items = items.OrderByDescending(r => r.DetailId).ToList();
                    break;
                case Sort.CodeAsc:
                    items = items.OrderBy(r => r.Code).ToList();
                    break;
                case Sort.CodeDesc:
                    items = items.OrderByDescending(r => r.Code).ToList();
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
                Details = items,
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(name)
            };

            return View(viewModel);
        }

        public async Task<IActionResult> IncomeSources()
        {
            return View(await context.Details.ToListAsync());
        }

        // GET: Units/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var source = await context.Details
                .FirstOrDefaultAsync(m => m.DetailId == id);
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
        public async Task<IActionResult> Create([Bind("DetailId,Code")] Detail source)
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

            var call = await context.Details.FindAsync(id);
            if (call == null)
            {
                return NotFound();
            }

            return View(call);
        }

        // POST: ExpenseTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DetailId,Code")] Detail source)
        {
            if (id != source.DetailId)
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
                    if (!DetailExists(source.DetailId))
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

            var call = await context.Details
                .FirstOrDefaultAsync(m => m.DetailId == id);
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
            var call = await context.Details.FindAsync(id);
            context.Details.Remove(call);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetailExists(int id)
        {
            return context.Details.Any(e => e.DetailId == id);
        }
    }
}
