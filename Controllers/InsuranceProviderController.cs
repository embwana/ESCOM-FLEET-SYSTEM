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
    public class InsuranceProviderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InsuranceProviderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InsuranceProvider
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.InsuranceProvider.ToListAsync());
        //}
        public async Task<IActionResult> Index(int? pageNumber, string sortOrder, string currentFilter, string searchString)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchString;
            //var applicationDbContext = _context.Station.ToListAsync();
            var insuranceproviders = await _context.InsuranceProvider.ToListAsync();
            var insuranceprovider = from s in _context.InsuranceProvider

                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                insuranceprovider = insuranceprovider.Where(s => s.ProviderName.Contains(searchString));
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
                return View(await PaginatedList<InsuranceProvider>.CreateAsync(insuranceprovider.AsNoTracking(), pageNumber ?? 1, pageSize));
            }
        }

        // GET: InsuranceProvider/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceProvider = await _context.InsuranceProvider
                .FirstOrDefaultAsync(m => m.InsuranceProviderId == id);
            if (insuranceProvider == null)
            {
                return NotFound();
            }

            return View(insuranceProvider);
        }

        // GET: InsuranceProvider/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InsuranceProvider/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InsuranceProviderId,ProviderName,ServiceOffered,EstablishedYear")] InsuranceProvider insuranceProvider)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insuranceProvider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(insuranceProvider);
        }

        // GET: InsuranceProvider/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceProvider = await _context.InsuranceProvider.FindAsync(id);
            if (insuranceProvider == null)
            {
                return NotFound();
            }
            return View(insuranceProvider);
        }

        // POST: InsuranceProvider/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InsuranceProviderId,ProviderName,ServiceOffered,EstablishedYear")] InsuranceProvider insuranceProvider)
        {
            if (id != insuranceProvider.InsuranceProviderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insuranceProvider);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuranceProviderExists(insuranceProvider.InsuranceProviderId))
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
            return View(insuranceProvider);
        }

        // GET: InsuranceProvider/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceProvider = await _context.InsuranceProvider
                .FirstOrDefaultAsync(m => m.InsuranceProviderId == id);
            if (insuranceProvider == null)
            {
                return NotFound();
            }

            return View(insuranceProvider);
        }

        // POST: InsuranceProvider/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var insuranceProvider = await _context.InsuranceProvider.FindAsync(id);
            _context.InsuranceProvider.Remove(insuranceProvider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsuranceProviderExists(int id)
        {
            return _context.InsuranceProvider.Any(e => e.InsuranceProviderId == id);
        }
    }
}
