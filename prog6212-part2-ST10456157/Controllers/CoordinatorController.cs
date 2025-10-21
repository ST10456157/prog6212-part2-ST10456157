using Microsoft.AspNetCore.Mvc;
using prog6212_part2_ST10456157.Models.prog6212_part2_ST10456157.Data;
using prog6212_part2_ST10456157.Models;

namespace prog6212_part1_ST10456157.Controllers
{
    public class CoordinatorController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CoordinatorController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public IActionResult Index() =>
            View(_context.Claims.Where(c => c.Status == "Pending" || c.Status == "Verified").ToList());

        [HttpPost]
        public IActionResult Verify(int id)
        {
            var claim = _context.Claims.Find(id);
            if (claim != null)
            {
                claim.Status = "Verified";
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Reject(int id)
        {
            var claim = _context.Claims.Find(id);
            if (claim != null)
            {
                claim.Status = "Rejected";
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
