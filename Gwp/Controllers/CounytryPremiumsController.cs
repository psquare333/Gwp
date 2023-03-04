using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gwp.Data;
using Gwp.Models;

namespace Gwp.Controllers
{
    public class CounytryPremiumsController : Controller
    {
        private readonly GwpContext _context;

        public CounytryPremiumsController(GwpContext context)
        {
            _context = context;
        }

        // GET: CounytryPremiums
        public async Task<IActionResult> Index()
        {
              return View(await _context.CounytryPremiums.ToListAsync());
        }

        // GET: CounytryPremiums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CounytryPremiums == null)
            {
                return NotFound();
            }

            var counytryPremiums = await _context.CounytryPremiums
                .FirstOrDefaultAsync(m => m.Id == id);
            if (counytryPremiums == null)
            {
                return NotFound();
            }

            return View(counytryPremiums);
        }

        // GET: CounytryPremiums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CounytryPremiums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Country,VariableId,LineOfBusiness,Year,Value")] CounytryPremiums counytryPremiums)
        {
            if (ModelState.IsValid)
            {
                _context.Add(counytryPremiums);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(counytryPremiums);
        }

        // GET: CounytryPremiums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CounytryPremiums == null)
            {
                return NotFound();
            }

            var counytryPremiums = await _context.CounytryPremiums.FindAsync(id);
            if (counytryPremiums == null)
            {
                return NotFound();
            }
            return View(counytryPremiums);
        }

        // POST: CounytryPremiums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Country,VariableId,LineOfBusiness,Year,Value")] CounytryPremiums counytryPremiums)
        {
            if (id != counytryPremiums.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(counytryPremiums);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CounytryPremiumsExists(counytryPremiums.Id))
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
            return View(counytryPremiums);
        }

        // GET: CounytryPremiums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CounytryPremiums == null)
            {
                return NotFound();
            }

            var counytryPremiums = await _context.CounytryPremiums
                .FirstOrDefaultAsync(m => m.Id == id);
            if (counytryPremiums == null)
            {
                return NotFound();
            }

            return View(counytryPremiums);
        }

        // POST: CounytryPremiums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CounytryPremiums == null)
            {
                return Problem("Entity set 'GwpContext.CounytryPremiums'  is null.");
            }
            var counytryPremiums = await _context.CounytryPremiums.FindAsync(id);
            if (counytryPremiums != null)
            {
                _context.CounytryPremiums.Remove(counytryPremiums);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CounytryPremiumsExists(int id)
        {
          return _context.CounytryPremiums.Any(e => e.Id == id);
        }
    }
}
