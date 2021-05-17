using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ESCOM_FLEET_SYSTEM.Data;
using ESCOM_FLEET_SYSTEM.Models;

namespace ESCOM_FLEET_SYSTEM.Controllers
{
    public class InsuranceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InsuranceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Insurance
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.Insurance.Include(i => i.InsuranceProvider);
        //    return View(await applicationDbContext.ToListAsync());
        //}

        public async Task<IActionResult> Index(int? pageNumber, string sortOrder, string currentFilter, string searchString)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchString;
            //var applicationDbContext = _context.Station.ToListAsync();
            var innsurances = await _context.Insurance.ToListAsync();
            var insurance = from s in _context.Insurance

                             select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                insurance = insurance.Where(s => s.InsuranceNumber.Contains(searchString));
                // || s.FirstMidName.Contains(searchString));
            }

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            {
                int pageSize = 5;
                return View(await PaginatedList<Insurance>.CreateAsync(insurance.AsNoTracking(), pageNumber ?? 1, pageSize));
            }
        }

        // GET: Insurance/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insurance = await _context.Insurance
                .Include(i => i.InsuranceProvider)
                .FirstOrDefaultAsync(m => m.InsuranceId == id);
            if (insurance == null)
            {
                return NotFound();
            }

            return View(insurance);
        }

        // GET: Insurance/Create
        public IActionResult Create()
        {
            ViewData["InsuranceProviderId"] = new SelectList(_context.Set<InsuranceProvider>(), "InsuranceProviderId", "ProviderName");
            return View();
        }

        // POST: Insurance/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InsuranceId,InsuranceNumber,Details,Issued,renewed,ProviderName")] Insurance insurance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insurance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InsuranceProviderId"] = new SelectList(_context.Set<InsuranceProvider>(), "InsuranceProviderId", "ProviderName", insurance.InsuranceProviderId);
            return View(insurance);
        }

        // GET: Insurance/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insurance = await _context.Insurance.FindAsync(id);
            if (insurance == null)
            {
                return NotFound();
            }
            ViewData["InsuranceProviderId"] = new SelectList(_context.Set<InsuranceProvider>(), "InsuranceProviderId", "ProviderName", insurance.InsuranceProviderId);
            return View(insurance);
        }

        // POST: Insurance/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InsuranceId,InsuranceNumber,Details,Issued,renewed,InsuranceProviderId")] Insurance insurance)
        {
            if (id != insurance.InsuranceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insurance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuranceExists(insurance.InsuranceId))
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
            ViewData["InsuranceProviderId"] = new SelectList(_context.Set<InsuranceProvider>(), "InsuranceProviderId", "ProviderName", insurance.InsuranceProviderId);
            return View(insurance);
        }

        // GET: Insurance/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insurance = await _context.Insurance
                .Include(i => i.InsuranceProvider)
                .FirstOrDefaultAsync(m => m.InsuranceId == id);
            if (insurance == null)
            {
                return NotFound();
            }

            return View(insurance);
        }

        // POST: Insurance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var insurance = await _context.Insurance.FindAsync(id);
            _context.Insurance.Remove(insurance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsuranceExists(int id)
        {
            return _context.Insurance.Any(e => e.InsuranceId == id);
        }
    }
}
