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
    public class FleetCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FleetCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FleetCategory
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.FleetCategory.Include(f => f.COFNumber).Include(f => f.DepartmentName).Include(f => f.DistrictName).Include(f => f.InsuranceNumber).Include(f => f.LocationName).Include(f => f.RegionName).Include(f => f.Station);
        //    return View(await applicationDbContext.ToListAsync());
        //}
        public async Task<IActionResult> Index(int? pageNumber, string sortOrder, string currentFilter, string searchString)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchString;
            //var applicationDbContext = _context.Station.ToListAsync();
            var fleetcategories = await _context.FleetCategory.ToListAsync();
            var fleetcategory = from s in _context.FleetCategory

                                    select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                fleetcategory = fleetcategory.Where(s => s.NumberPlate.Contains(searchString));
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
                return View(await PaginatedList<FleetCategory>.CreateAsync(fleetcategory.AsNoTracking(), pageNumber ?? 1, pageSize));
            }
        }
        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> Search(string term)
        {
            if (!string.IsNullOrEmpty(term))
            {
                var states = await _context.COF.ToListAsync();
                var data = states.Where(a => a.COFNumber.Contains(term, StringComparison.OrdinalIgnoreCase)
                || a.COFNumber.Contains(term, StringComparison.OrdinalIgnoreCase)
                || a.COFNumber.Contains(term, StringComparison.OrdinalIgnoreCase)).ToList().AsReadOnly();
                return Ok(data);
            }
            else
            {
                return Ok();
            }
        }
        // GET: FleetCategory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fleetCategory = await _context.FleetCategory
                .Include(f => f.COFNumber)
                .Include(f => f.DepartmentName)
                .Include(f => f.DistrictName)
                .Include(f => f.InsuranceNumber)
                .Include(f => f.LocationName)
                .Include(f => f.RegionName)
                .Include(f => f.Station)
                .FirstOrDefaultAsync(m => m.FleetCategoryId == id);
            if (fleetCategory == null)
            {
                return NotFound();
            }

            return View(fleetCategory);
        }

        // GET: FleetCategory/Create
        public IActionResult Create()
        {
            ViewData["COFId"] = new SelectList(_context.COF, "COFId", "COFNumber");
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "DepartmentName");
            ViewData["DistrictId"] = new SelectList(_context.District, "DistrictId", "DistrictName");
            ViewData["InsuranceId"] = new SelectList(_context.Set<Insurance>(), "InsuranceId", "InsuranceNumber");
            ViewData["LocationId"] = new SelectList(_context.Location, "LocationId", "LocationName");
            ViewData["RegionId"] = new SelectList(_context.Set<Region>(), "RegionId", "RegionName");
            ViewData["StationId"] = new SelectList(_context.Set<Station>(), "StationId", "StationName");
            return View();
        }

        // POST: FleetCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FleetCategoryId,NumberPlate,Model,Year,Cost,TagNumber,Mileage,COFId,InsuranceId,StationId,LocationId,DistrictId,RegionId,DepartmentId")] FleetCategory fleetCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fleetCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["COFId"] = new SelectList(_context.COF, "COFId", "COFNumber", fleetCategory.COFId);
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "DepartmentName", fleetCategory.DepartmentId);
            ViewData["DistrictId"] = new SelectList(_context.District, "DistrictId", "DistrictName", fleetCategory.DistrictId);
            ViewData["InsuranceId"] = new SelectList(_context.Set<Insurance>(), "InsuranceId", "InsuranceNumber", fleetCategory.InsuranceId);
            ViewData["LocationId"] = new SelectList(_context.Location, "LocationId", "LocationName", fleetCategory.LocationId);
            ViewData["RegionId"] = new SelectList(_context.Set<Region>(), "RegionId", "RegionName", fleetCategory.RegionId);
            ViewData["StationId"] = new SelectList(_context.Set<Station>(), "StationId", "StationName", fleetCategory.StationId);
            return View(fleetCategory);
        }

        // GET: FleetCategory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fleetCategory = await _context.FleetCategory.FindAsync(id);
            if (fleetCategory == null)
            {
                return NotFound();
            }
            ViewData["COFId"] = new SelectList(_context.COF, "COFId", "COFNumber", fleetCategory.COFId);
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "DepartmentName", fleetCategory.DepartmentId);
            ViewData["DistrictId"] = new SelectList(_context.District, "DistrictId", "DistrictName", fleetCategory.DistrictId);
            ViewData["InsuranceId"] = new SelectList(_context.Set<Insurance>(), "InsuranceId", "InsuranceNumber", fleetCategory.InsuranceId);
            ViewData["LocationId"] = new SelectList(_context.Location, "LocationId", "LocationName", fleetCategory.LocationId);
            ViewData["RegionId"] = new SelectList(_context.Set<Region>(), "RegionId", "RegionName", fleetCategory.RegionId);
            ViewData["StationId"] = new SelectList(_context.Set<Station>(), "StationId", "StationName", fleetCategory.StationId);
            return View(fleetCategory);
        }

        // POST: FleetCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FleetCategoryId,NumberPlate,Model,Year,Cost,TagNumber,Mileage,COFId,InsuranceId,StationId,LocationId,DistrictId,RegionId,DepartmentId")] FleetCategory fleetCategory)
        {
            if (id != fleetCategory.FleetCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fleetCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FleetCategoryExists(fleetCategory.FleetCategoryId))
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
            ViewData["COFId"] = new SelectList(_context.COF, "COFId", "COFNumber", fleetCategory.COFId);
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "DepartmentName", fleetCategory.DepartmentId);
            ViewData["DistrictId"] = new SelectList(_context.District, "DistrictId", "DistrictName", fleetCategory.DistrictId);
            ViewData["InsuranceId"] = new SelectList(_context.Set<Insurance>(), "InsuranceId", "InsuranceNumber", fleetCategory.InsuranceId);
            ViewData["LocationId"] = new SelectList(_context.Location, "LocationId", "LocationName", fleetCategory.LocationId);
            ViewData["RegionId"] = new SelectList(_context.Set<Region>(), "RegionId", "RegionName", fleetCategory.RegionId);
            ViewData["StationId"] = new SelectList(_context.Set<Station>(), "StationId", "StationName", fleetCategory.StationId);
            return View(fleetCategory);
        }

        // GET: FleetCategory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fleetCategory = await _context.FleetCategory
                .Include(f => f.COFNumber)
                .Include(f => f.DepartmentName)
                .Include(f => f.DistrictName)
                .Include(f => f.InsuranceNumber)
                .Include(f => f.LocationName)
                .Include(f => f.RegionName)
                .Include(f => f.Station)
                .FirstOrDefaultAsync(m => m.FleetCategoryId == id);
            if (fleetCategory == null)
            {
                return NotFound();
            }

            return View(fleetCategory);
        }

        // POST: FleetCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fleetCategory = await _context.FleetCategory.FindAsync(id);
            _context.FleetCategory.Remove(fleetCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FleetCategoryExists(int id)
        {
            return _context.FleetCategory.Any(e => e.FleetCategoryId == id);
        }
    }
}
