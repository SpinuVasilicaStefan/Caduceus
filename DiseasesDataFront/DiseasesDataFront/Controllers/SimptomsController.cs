using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DiseasesDataFront.Data;
using DiseasesDataFront.Models;

namespace DiseasesDataFront.Controllers
{
    public class SimptomsController : Controller
    {
        private readonly MvcSimptomContext _context;

        public SimptomsController(MvcSimptomContext context)
        {
            _context = context;
        }

        // GET: Simptoms
        public async Task<IActionResult> Index()
        {
            return View(await _context.Simptom.ToListAsync());
        }

        // GET: Simptoms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var simptom = await _context.Simptom
                .FirstOrDefaultAsync(m => m.Id == id);
            if (simptom == null)
            {
                return NotFound();
            }

            return View(simptom);
        }

        public async Task<IActionResult> ListSimptoms(int? id)
        {
            var s =  _context.Simptom.ToList();

            return View(s);
        }

        // GET: Simptoms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Simptoms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Simptom simptom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(simptom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(simptom);
        }

        // GET: Simptoms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var simptom = await _context.Simptom.FindAsync(id);
            if (simptom == null)
            {
                return NotFound();
            }
            return View(simptom);
        }

        // POST: Simptoms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Simptom simptom)
        {
            if (id != simptom.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(simptom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SimptomExists(simptom.Id))
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
            return View(simptom);
        }

        // GET: Simptoms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var simptom = await _context.Simptom
                .FirstOrDefaultAsync(m => m.Id == id);
            if (simptom == null)
            {
                return NotFound();
            }

            return View(simptom);
        }

        // POST: Simptoms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var simptom = await _context.Simptom.FindAsync(id);
            _context.Simptom.Remove(simptom);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SimptomExists(int id)
        {
            return _context.Simptom.Any(e => e.Id == id);
        }
    }
}
