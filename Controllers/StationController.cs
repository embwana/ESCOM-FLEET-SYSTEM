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
    public class StationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Station
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.Station.Include(s => s.DepartmentName).Include(s => s.DistrictName).Include(s => s.LocationName).Include(s => s.RegionName);
        //    return View(await applicationDbContext.ToListAsync());
        public async Task<IActionResult> Index(int? pageNumber, string sortOrder, string currentFilter, string searchString)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchString;
            //var applicationDbContext = _context.Station.ToListAsync();
            var stations = await _context.Station.ToListAsync();
            var station = from s in _context.Station
                          .Include(c => c.DepartmentName)
                          .AsNoTracking()
                          select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                station = station.Where(s => s.StationName.Contains(searchString));
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
                return View(await PaginatedList<Station>.CreateAsync(station.AsNoTracking(), pageNumber ?? 1, pageSize));
            }
        }

        // GET: Station/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var station = await _context.Station
                .Include(s => s.LocationName)
                .Include(s => s.DepartmentName)
                .Include(s => s.DistrictName)
                .Include(s => s.RegionName)
                .FirstOrDefaultAsync(m => m.StationId == id);
            if (station == null)
            {
                return NotFound();
            }

            return View(station);
        }

        // GET: Station/Create
        public IActionResult Create()
        {
            ViewData["LocationId"] = new SelectList(_context.Location, "LocationId", "LocationName");
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "DepartmentName");
            ViewData["DistrictId"] = new SelectList(_context.District, "DistrictId", "DistrictName");
            ViewData["RegionId"] = new SelectList(_context.Region, "RegionId", "RegionName");
            return View();
        }

        // POST: Station/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StationId,StationName,LocationId,DepartmentId,DistrictId,RegionId")] Station station)
        {
            if (ModelState.IsValid)
            {
                _context.Add(station);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new SelectList(_context.Location, "LocationId", "LocationName", station.LocationId);
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "DepartmentName", station.DepartmentId);
            ViewData["DistrictId"] = new SelectList(_context.District, "DistrictId", "DistrictName", station.DistrictId);
            ViewData["RegionId"] = new SelectList(_context.Region, "RegionId", "RegionName", station.RegionId);
            return View(station);
        }

        // GET: Station/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var station = await _context.Station.FindAsync(id);
            if (station == null)
            {
                return NotFound();
            }
            ViewData["LocationId"] = new SelectList(_context.Location, "LocationId", "LocationName", station.LocationId);
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "Comment", station.DepartmentId);
            ViewData["DistrictId"] = new SelectList(_context.District, "DistrictId", "LocationName", station.DistrictId);
            ViewData["RegionId"] = new SelectList(_context.Region, "RegionId", "RegionName", station.RegionId);
            return View(station);
        }

        // POST: Station/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StationId,StationName,LocationId,DepartmentId,DistrictId,RegionId")] Station station)
        {
            if (id != station.StationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(station);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StationExists(station.StationId))
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
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "DepartmentName", station.DepartmentId);
            ViewData["DistrictId"] = new SelectList(_context.District, "DistrictId", "DistrictName", station.DistrictId);
            ViewData["LocationId"] = new SelectList(_context.Location, "LocationId", "LocationName", station.LocationId);
            ViewData["RegionId"] = new SelectList(_context.Region, "RegionId", "RegionName", station.RegionId);
            return View(station);
        }

        // GET: Station/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var station = await _context.Station
                .Include(s => s.DepartmentName)
                .Include(s => s.DistrictName)
                .Include(s => s.LocationName)
                .Include(s => s.RegionName)
                .FirstOrDefaultAsync(m => m.StationId == id);
            if (station == null)
            {
                return NotFound();
            }

            return View(station);
        }

        // POST: Station/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var station = await _context.Station.FindAsync(id);
            _context.Station.Remove(station);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StationExists(int id)
        {
            return _context.Station.Any(e => e.StationId == id);
        }
    }
}
