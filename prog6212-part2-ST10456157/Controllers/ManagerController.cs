using Microsoft.AspNetCore.Mvc;
using prog6212_part2_ST10456157.Models.prog6212_part2_ST10456157.Data;
using prog6212_part2_ST10456157.Models;

namespace prog6212_part1_ST10456157.Controllers
{
    public class ManagerController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ManagerController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public IActionResult Index() =>
            View(_context.Claims.Where(c => c.Status == "Verified" || c.Status == "Approved").ToList());

        [HttpPost]
        public IActionResult Approve(int id)
        {
            var claim = _context.Claims.Find(id);
            if (claim != null)
            {
                claim.Status = "Approved";
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
