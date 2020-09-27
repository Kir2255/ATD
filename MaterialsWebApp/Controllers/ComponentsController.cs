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
    public class ComponentsController : Controller
    {
        private readonly RecordKeepingContext context;
        public ComponentsController(RecordKeepingContext _context)
        {
            context = _context;
        }

        // GET: Units
        [ResponseCache(Duration = 252)]
        public async Task<IActionResult> Index(string name, int page = 1, Sort sortOrder = Sort.IdAsc)
        {
            int pageSize = 20;


            List<Component> items = await context.Components.Include(r => r.Detail).Include(r => r.Material).Include(r => r.Unit).ToListAsync();
            if (!String.IsNullOrEmpty(name))
            {
                items = items.Where(r => r.Material.Name.Contains(name)).ToList();
            }

            switch (sortOrder)
            {
                case Sort.IdAsc:
                    items = items.OrderBy(r => r.ComponentId).ToList();
                    break;
                case Sort.IdDesc:
                    items = items.OrderByDescending(r => r.ComponentId).ToList();
                    break;
                case Sort.CodeAsc:
                    items = items.OrderBy(r => r.Detail.Code).ToList();
                    break;
                case Sort.CodeDesc:
                    items = items.OrderByDescending(r => r.Detail.Code).ToList();
                    break;
                case Sort.NameAsc:
                    items = items.OrderBy(r => r.Material.Name).ToList();
                    break;
                case Sort.NameDesc:
                    items = items.OrderByDescending(r => r.Material.Name).ToList();
                    break;
                case Sort.UnitAsc:
                    items = items.OrderBy(r => r.Unit.Description).ToList();
                    break;
                case Sort.UnitDesc:
                    items = items.OrderByDescending(r => r.Unit.Description).ToList();
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
                Components = items,
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(name)
            };

            return View(viewModel);
        }

        public async Task<IActionResult> IncomeSources()
        {
            return View(await context.Components.ToListAsync());
        }

        // GET: Units/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var source = await context.Components.Include(r => r.Detail).Include(r => r.Material).Include(r => r.Unit)
                .FirstOrDefaultAsync(m => m.ComponentId == id);
            if (source == null)
            {
                return NotFound();
            }

            return View(source);
        }

        // GET: Units/Create
        public IActionResult Create()
        {
            ViewData["DetailId"] = new SelectList(context.Details, "DetailId", "Code");
            ViewData["MaterialId"] = new SelectList(context.Materials, "MaterialId", "Name");
            ViewData["UnitId"] = new SelectList(context.Units, "UnitId", "Description");
            return View();
        }

        // POST: Units/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComponentId,DetailId,MaterialId,UnitId")] Component source)
        {
            if (ModelState.IsValid)
            {
                context.Add(source);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["DetailId"] = new SelectList(context.Details, "DetailId", "Code", source.DetailId);
            ViewData["MaterialId"] = new SelectList(context.Materials, "MaterialId", "Name", source.MaterialId);
            ViewData["UnitId"] = new SelectList(context.Units, "UnitId", "Description", source.UnitId);
            return View(source);
        }

        // GET: ExpenseTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var call = await context.Components.FindAsync(id);
            if (call == null)
            {
                return NotFound();
            }

            ViewData["DetailId"] = new SelectList(context.Details, "DetailId", "Code", call.DetailId);
            ViewData["MaterialId"] = new SelectList(context.Materials, "MaterialId", "Name", call.MaterialId);
            ViewData["UnitId"] = new SelectList(context.Units, "UnitId", "Description", call.UnitId);
            return View(call);
        }

        // POST: ExpenseTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ComponentId,DetailId,MaterialId,UnitId")] Component source)
        {
            if (id != source.ComponentId)
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
                    if (!ComponentExists(source.ComponentId))
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

            ViewData["DetailId"] = new SelectList(context.Details, "DetailId", "Code", source.DetailId);
            ViewData["MaterialId"] = new SelectList(context.Materials, "MaterialId", "Name", source.MaterialId);
            ViewData["UnitId"] = new SelectList(context.Units, "UnitId", "Description", source.UnitId);
            return View(source);
        }

        // GET: ExpenseTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var call = await context.Components.Include(r => r.Detail).Include(r => r.Material).Include(r => r.Unit)
                .FirstOrDefaultAsync(m => m.ComponentId == id);
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
            var call = await context.Components.FindAsync(id);
            context.Components.Remove(call);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComponentExists(int id)
        {
            return context.Components.Any(e => e.ComponentId == id);
        }
    }
}
