using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CallsCRM.Models;

namespace CallsCRM.Controllers
{
    public class CallerController : Controller
    {
        private readonly CustomerContext _context;

        public CallerController(CustomerContext context)
        {
            _context = context;
        }

        // GET: Caller
        public async Task<IActionResult> Index()
        {
            return View(await _context.Callers.ToListAsync());
        }

        // GET: Caller/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caller = await _context.Callers
                .SingleOrDefaultAsync(m => m.CallerId == id);
            if (caller == null)
            {
                return NotFound();
            }

            return View(caller);
        }

        // GET: Caller/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Caller/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CallerId,Login,Name,LastName")] Caller caller)
        {
            if (ModelState.IsValid && _context.Callers.All(c => c.Login != caller.Login))
            {
                _context.Add(caller);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(caller);
        }

        // GET: Caller/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caller = await _context.Callers.SingleOrDefaultAsync(m => m.CallerId == id);
            if (caller == null)
            {
                return NotFound();
            }
            return View(caller);
        }

        // POST: Caller/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CallerId,Login,Name,LastName")] Caller caller)
        {
            if (id != caller.CallerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid && _context.Callers.All(c => c.Login != caller.Login))
            {
                try
                {
                    _context.Update(caller);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CallerExists(caller.CallerId))
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
            return View(caller);
        }

        // GET: Caller/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caller = await _context.Callers
                .SingleOrDefaultAsync(m => m.CallerId == id);
            if (caller == null)
            {
                return NotFound();
            }

            return View(caller);
        }

        // POST: Caller/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var caller = await _context.Callers.SingleOrDefaultAsync(m => m.CallerId == id);
            _context.Callers.Remove(caller);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CallerExists(int id)
        {
            return _context.Callers.Any(e => e.CallerId == id);
        }
    }
}
