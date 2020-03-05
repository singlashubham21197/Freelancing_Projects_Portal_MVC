using Freelancing_Projects_Portal_MVC.Data;
using Freelancing_Projects_Portal_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Freelancing_Projects_Portal_MVC.Controllers
{
    public class BidsController : Controller
    {
        private readonly Freelancing_Projects_Portal_DBContext _context;

        public BidsController(Freelancing_Projects_Portal_DBContext context)
        {
            _context = context;
        }

        // GET: Bids
        public async Task<IActionResult> Index()
        {
            var freelancing_Projects_Portal_DBContext = _context.Bid.Include(b => b.Developer).Include(b => b.Project);
            return View(await freelancing_Projects_Portal_DBContext.ToListAsync());
        }

        // GET: Bids/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bid = await _context.Bid
                .Include(b => b.Developer)
                .Include(b => b.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bid == null)
            {
                return NotFound();
            }

            return View(bid);
        }
        [Authorize]
        // GET: Bids/Create
        public IActionResult Create()
        {
            ViewData["DeveloperId"] = new SelectList(_context.Set<Developer>(), "Id", "Id");
            ViewData["ProjectId"] = new SelectList(_context.Set<Project>(), "Id", "Id");
            return View();
        }

        // POST: Bids/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BidValue,ProjectId,DeveloperId")] Bid bid)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bid);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeveloperId"] = new SelectList(_context.Set<Developer>(), "Id", "Id", bid.DeveloperId);
            ViewData["ProjectId"] = new SelectList(_context.Set<Project>(), "Id", "Id", bid.ProjectId);
            return View(bid);
        }
        [Authorize]
        // GET: Bids/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bid = await _context.Bid.FindAsync(id);
            if (bid == null)
            {
                return NotFound();
            }
            ViewData["DeveloperId"] = new SelectList(_context.Set<Developer>(), "Id", "Id", bid.DeveloperId);
            ViewData["ProjectId"] = new SelectList(_context.Set<Project>(), "Id", "Id", bid.ProjectId);
            return View(bid);
        }

        // POST: Bids/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BidValue,ProjectId,DeveloperId")] Bid bid)
        {
            if (id != bid.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bid);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BidExists(bid.Id))
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
            ViewData["DeveloperId"] = new SelectList(_context.Set<Developer>(), "Id", "Id", bid.DeveloperId);
            ViewData["ProjectId"] = new SelectList(_context.Set<Project>(), "Id", "Id", bid.ProjectId);
            return View(bid);
        }
        [Authorize]
        // GET: Bids/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bid = await _context.Bid
                .Include(b => b.Developer)
                .Include(b => b.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bid == null)
            {
                return NotFound();
            }

            return View(bid);
        }

        // POST: Bids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bid = await _context.Bid.FindAsync(id);
            _context.Bid.Remove(bid);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BidExists(int id)
        {
            return _context.Bid.Any(e => e.Id == id);
        }
    }
}
