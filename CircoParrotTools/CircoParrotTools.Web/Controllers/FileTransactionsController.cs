using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CircoParrotTools.Data.Models.DB;

namespace CircoParrotTools.Web
{
    public class FileTransactionsController : Controller
    {
        private readonly CircoParrotContext _context;

        public FileTransactionsController(CircoParrotContext context)
        {
            _context = context;
        }

        // GET: FileTransactions
        public async Task<IActionResult> Index()
        {
            return View(await _context.FileTransactions.ToListAsync());
        }

        // GET: FileTransactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileTransactions = await _context.FileTransactions
                .SingleOrDefaultAsync(m => m.PurchaseOrderId == id);
            if (fileTransactions == null)
            {
                return NotFound();
            }

            return View(fileTransactions);
        }

        // GET: FileTransactions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FileTransactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PurchaseOrderId,LineNumber,ProductId")] FileTransactions fileTransactions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fileTransactions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fileTransactions);
        }

        // GET: FileTransactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileTransactions = await _context.FileTransactions.SingleOrDefaultAsync(m => m.PurchaseOrderId == id);
            if (fileTransactions == null)
            {
                return NotFound();
            }
            return View(fileTransactions);
        }

        // POST: FileTransactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PurchaseOrderId,LineNumber,ProductId")] FileTransactions fileTransactions)
        {
            if (id != fileTransactions.PurchaseOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fileTransactions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FileTransactionsExists(fileTransactions.PurchaseOrderId))
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
            return View(fileTransactions);
        }

        // GET: FileTransactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileTransactions = await _context.FileTransactions
                .SingleOrDefaultAsync(m => m.PurchaseOrderId == id);
            if (fileTransactions == null)
            {
                return NotFound();
            }

            return View(fileTransactions);
        }

        // POST: FileTransactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fileTransactions = await _context.FileTransactions.SingleOrDefaultAsync(m => m.PurchaseOrderId == id);
            _context.FileTransactions.Remove(fileTransactions);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FileTransactionsExists(int id)
        {
            return _context.FileTransactions.Any(e => e.PurchaseOrderId == id);
        }
    }
}
