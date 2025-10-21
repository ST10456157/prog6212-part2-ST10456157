using Microsoft.AspNetCore.Mvc;
using prog6212_part2_ST10456157.Models.prog6212_part2_ST10456157.Data;
using System.Security.Claims;
using prog6212_part2_ST10456157.Models;
using Microsoft.EntityFrameworkCore;


namespace prog6212_part1_ST10456157.Controllers
{
    public class LecturerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public LecturerController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var claims = _context.Claims.ToList();
            return View(claims);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitClaim(prog6212_part2_ST10456157.Models.Claim model, IFormFile? file)
        {
            try
            {
                if (file != null && file.Length > 0)
                {
                    var uploadPath = Path.Combine(_env.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadPath))
                        Directory.CreateDirectory(uploadPath);

                    var filePath = Path.Combine(uploadPath, file.FileName);
                    using var stream = new FileStream(filePath, FileMode.Create);
                    await file.CopyToAsync(stream);
                    model.DocumentName = file.FileName;
                }

                model.Status = "Pending";
                _context.Claims.Add(model);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Claim submitted successfully!";
            }
            catch
            {
                TempData["Error"] = "Error submitting claim.";
            }
            return RedirectToAction("Index");
        }
    }
}
