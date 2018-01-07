using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CallsCRM.Models;
using CallsCRM.Strategy;

namespace CallsCRM.Controllers
{
    public class CallController : Controller
    {
        private readonly CustomerContext _context;
        private readonly ICallStrategy _callStrategy;

        public CallController(CustomerContext context, ICallStrategy callStrategy)
        {
            _context = context;
            _callStrategy = callStrategy;
        }

        // GET: Call
        public async Task<IActionResult> Index()
        {
            var customerContext = _context.Calls.Include(c => c.Callee).Include(c => c.Caller);
            return View(await customerContext.ToListAsync());
        }

        // GET: Call/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var call = await _context.Calls
                .Include(c => c.Callee)
                .Include(c => c.Caller)
                .SingleOrDefaultAsync(m => m.CallId == id);
            if (call == null)
            {
                return NotFound();
            }

            return View(call);
        }

        // GET: Call/Create
        public IActionResult Create(int? id)
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FullName", id);
            ViewData["CallerId"] = new SelectList(_context.Callers, "CallerId", "Login");
            return View();
        }

        // POST: Call/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,CallerId")] Call call)
        {
            call.Time = new CallTime(DateTime.Now, _callStrategy.Call());
            if (ModelState.IsValid)
            {
                _context.Add(call);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Email", call.CustomerId);
            ViewData["CallerId"] = new SelectList(_context.Callers, "CallerId", "Login", call.CallerId);
            return View(call);
        }

        // GET: Call/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var call = await _context.Calls
                .Include(c => c.Callee)
                .Include(c => c.Caller)
                .SingleOrDefaultAsync(m => m.CallId == id);
            if (call == null)
            {
                return NotFound();
            }

            return View(call);
        }

        // POST: Call/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var call = await _context.Calls.SingleOrDefaultAsync(m => m.CallId == id);
            _context.Calls.Remove(call);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
