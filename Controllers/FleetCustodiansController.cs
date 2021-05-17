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
    public class FleetCustodiansController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FleetCustodiansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FleetCustodians
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.FleetCustodian.Include(f => f.COFNumber).Include(f => f.DepartmentName).Include(f => f.LicenceNumber).Include(f => f.NumberPlate).Include(f => f.StationName);
        //    return View(await applicationDbContext.ToListAsync());
        //}
        public async Task<IActionResult> Index(int? pageNumber, string sortOrder, string currentFilter, string searchString)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchString;
            //var applicationDbContext = _context.Station.ToListAsync();
            var fleetcustodians = await _context.FleetCustodian.ToListAsync();
            var fleetcustodian = from s in _context.FleetCustodian
                          //.Include(c => c.CollectedOn)
                          //.AsNoTracking()
                          select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                //fleetcustodian = fleetcustodian.Where(s => s.CollectedOn.Contains(searchString));
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
                return View(await PaginatedList<FleetCustodian>.CreateAsync(fleetcustodian.AsNoTracking(), pageNumber ?? 1, pageSize));
            }
        }

            // GET: FleetCustodians/Details/5
            public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fleetCustodian = await _context.FleetCustodian
                .Include(f => f.COFNumber)
                .Include(f => f.DepartmentName)
                .Include(f => f.LicenceNumber)
                .Include(f => f.NumberPlate)
                .Include(f => f.StationName)
                .FirstOrDefaultAsync(m => m.FleetCustodianId == id);
            if (fleetCustodian == null)
            {
                return NotFound();
            }

            return View(fleetCustodian);
        }

        // GET: FleetCustodians/Create
        public IActionResult Create()
        {
            ViewData["COFId"] = new SelectList(_context.COF, "COFId", "COFNumber");
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "Comment");
            ViewData["LicenceId"] = new SelectList(_context.Licences, "LicenceId", "Department");
            ViewData["FleetCategoryId"] = new SelectList(_context.FleetCategory, "FleetCategoryId", "Mileage");
            ViewData["StationId"] = new SelectList(_context.Set<Station>(), "StationId", "StationName");
            return View();
        }

        // POST: FleetCustodians/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FleetCustodianId,LicenceId,COFId,FleetCategoryId,StationId,DepartmentId,CollectedOn,ReturnedOn")] FleetCustodian fleetCustodian)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fleetCustodian);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["COFId"] = new SelectList(_context.COF, "COFId", "COFNumber", fleetCustodian.COFId);
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "Comment", fleetCustodian.DepartmentId);
            ViewData["LicenceId"] = new SelectList(_context.Licences, "LicenceId", "Department", fleetCustodian.LicenceId);
            ViewData["FleetCategoryId"] = new SelectList(_context.FleetCategory, "FleetCategoryId", "Mileage", fleetCustodian.FleetCategoryId);
            ViewData["StationId"] = new SelectList(_context.Set<Station>(), "StationId", "StationName", fleetCustodian.StationId);
            return View(fleetCustodian);
        }

        // GET: FleetCustodians/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fleetCustodian = await _context.FleetCustodian.FindAsync(id);
            if (fleetCustodian == null)
            {
                return NotFound();
            }
            ViewData["COFId"] = new SelectList(_context.COF, "COFId", "COFNumber", fleetCustodian.COFId);
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "Comment", fleetCustodian.DepartmentId);
            ViewData["LicenceId"] = new SelectList(_context.Licences, "LicenceId", "Department", fleetCustodian.LicenceId);
            ViewData["FleetCategoryId"] = new SelectList(_context.FleetCategory, "FleetCategoryId", "Mileage", fleetCustodian.FleetCategoryId);
            ViewData["StationId"] = new SelectList(_context.Set<Station>(), "StationId", "StationName", fleetCustodian.StationId);
            return View(fleetCustodian);
        }

        // POST: FleetCustodians/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FleetCustodianId,LicenceId,COFId,FleetCategoryId,StationId,DepartmentId,CollectedOn,ReturnedOn")] FleetCustodian fleetCustodian)
        {
            if (id != fleetCustodian.FleetCustodianId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fleetCustodian);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FleetCustodianExists(fleetCustodian.FleetCustodianId))
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
            ViewData["COFId"] = new SelectList(_context.COF, "COFId", "COFNumber", fleetCustodian.COFId);
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "Comment", fleetCustodian.DepartmentId);
            ViewData["LicenceId"] = new SelectList(_context.Licences, "LicenceId", "Department", fleetCustodian.LicenceId);
            ViewData["FleetCategoryId"] = new SelectList(_context.FleetCategory, "FleetCategoryId", "Mileage", fleetCustodian.FleetCategoryId);
            ViewData["StationId"] = new SelectList(_context.Set<Station>(), "StationId", "StationName", fleetCustodian.StationId);
            return View(fleetCustodian);
        }

        // GET: FleetCustodians/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fleetCustodian = await _context.FleetCustodian
                .Include(f => f.COFNumber)
                .Include(f => f.DepartmentName)
                .Include(f => f.LicenceNumber)
                .Include(f => f.NumberPlate)
                .Include(f => f.StationName)
                .FirstOrDefaultAsync(m => m.FleetCustodianId == id);
            if (fleetCustodian == null)
            {
                return NotFound();
            }

            return View(fleetCustodian);
        }

        // POST: FleetCustodians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fleetCustodian = await _context.FleetCustodian.FindAsync(id);
            _context.FleetCustodian.Remove(fleetCustodian);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FleetCustodianExists(int id)
        {
            return _context.FleetCustodian.Any(e => e.FleetCustodianId == id);
        }
    }
}
