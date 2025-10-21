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
        [HttpPost]
        public async Task<IActionResult> SubmitClaim(prog6212_part2_ST10456157.Models.Claim model, IFormFile? file)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Error"] = "Invalid claim data. Please check your input.";
                    return RedirectToAction("Index");
                }

                // 
                if (file != null && file.Length > 0)
                {
                    //  Allowed file types
                    var allowedExtensions = new[] { ".pdf" };
                    var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        TempData["Error"] = $"File type '{fileExtension}' is not supported. " +
                                            "Please upload a PDF, Word, Excel, or image file.";
                        return RedirectToAction("Index");
                    }

                    // 2 Validate file size 
                    const long maxSizeInBytes = 5 * 1024 * 1024; // 5MB
                    if (file.Length > maxSizeInBytes)
                    {
                        TempData["Error"] = "File is too large. Maximum size allowed is 5MB.";
                        return RedirectToAction("Index");
                    }

                    //  Save file if valid
                    var uploadPath = Path.Combine(_env.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadPath))
                        Directory.CreateDirectory(uploadPath);

                    var safeFileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine(uploadPath, safeFileName);

                    using var stream = new FileStream(filePath, FileMode.Create);
                    await file.CopyToAsync(stream);

                    model.DocumentName = safeFileName;
                }

                model.Status = "Pending";
                _context.Claims.Add(model);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Claim submitted successfully!";
            }
            catch (IOException)
            {
                TempData["Error"] = "There was a problem saving your document. Please try again.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Unexpected error: " + ex.Message;
            }

            return RedirectToAction("Index");
        }

    }
}
