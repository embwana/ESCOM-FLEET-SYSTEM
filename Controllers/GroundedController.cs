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
    public class GroundedController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GroundedController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Grounded
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.Grounded.Include(g => g.Department).Include(g => g.NumberPlate).Include(g => g.Station);
        //    return View(await applicationDbContext.ToListAsync());
        //}

        public async Task<IActionResult> Index(int? pageNumber, string sortOrder, string currentFilter, string searchString)
        {
            //return View(await _context.COF.ToListAsync());
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchString;
            //var applicationDbContext = _context.Licence.ToListAsync();
            var groundeds = await _context.Grounded.ToListAsync();
            var grounded = from s in _context.Grounded
                      select s;

            if (!String.IsNullOrEmpty(searchString))
            {
               // groundeds = groundeds.Where(s => s.NumberPlate.Contains(searchString));
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
                int pageSize = 7;
                return View(await PaginatedList<Grounded>.CreateAsync(grounded.AsNoTracking(), pageNumber ?? 1, pageSize));


            }
        }


        // GET: Grounded/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grounded = await _context.Grounded
                .Include(g => g.Department)
                .Include(g => g.NumberPlate)
                .Include(g => g.Station)
                .FirstOrDefaultAsync(m => m.GroundedId == id);
            if (grounded == null)
            {
                return NotFound();
            }

            return View(grounded);
        }

        // GET: Grounded/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "Comment");
            ViewData["FleetCategoryId"] = new SelectList(_context.FleetCategory, "FleetCategoryId", "Mileage");
            ViewData["StationId"] = new SelectList(_context.Set<Station>(), "StationId", "StationName");
            return View();
        }

        // POST: Grounded/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GroundedId,FleetCategoryId,DepartmentId,StationId,Remarks")] Grounded grounded)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grounded);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "Comment", grounded.DepartmentId);
            ViewData["FleetCategoryId"] = new SelectList(_context.FleetCategory, "FleetCategoryId", "Mileage", grounded.FleetCategoryId);
            ViewData["StationId"] = new SelectList(_context.Set<Station>(), "StationId", "StationName", grounded.StationId);
            return View(grounded);
        }

        // GET: Grounded/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grounded = await _context.Grounded.FindAsync(id);
            if (grounded == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "Comment", grounded.DepartmentId);
            ViewData["FleetCategoryId"] = new SelectList(_context.FleetCategory, "FleetCategoryId", "Mileage", grounded.FleetCategoryId);
            ViewData["StationId"] = new SelectList(_context.Set<Station>(), "StationId", "StationName", grounded.StationId);
            return View(grounded);
        }

        // POST: Grounded/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GroundedId,FleetCategoryId,DepartmentId,StationId,Remarks")] Grounded grounded)
        {
            if (id != grounded.GroundedId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grounded);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroundedExists(grounded.GroundedId))
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
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "Comment", grounded.DepartmentId);
            ViewData["FleetCategoryId"] = new SelectList(_context.FleetCategory, "FleetCategoryId", "Mileage", grounded.FleetCategoryId);
            ViewData["StationId"] = new SelectList(_context.Set<Station>(), "StationId", "StationName", grounded.StationId);
            return View(grounded);
        }

        // GET: Grounded/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grounded = await _context.Grounded
                .Include(g => g.Department)
                .Include(g => g.NumberPlate)
                .Include(g => g.Station)
                .FirstOrDefaultAsync(m => m.GroundedId == id);
            if (grounded == null)
            {
                return NotFound();
            }

            return View(grounded);
        }

        // POST: Grounded/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grounded = await _context.Grounded.FindAsync(id);
            _context.Grounded.Remove(grounded);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroundedExists(int id)
        {
            return _context.Grounded.Any(e => e.GroundedId == id);
        }
    }
}
