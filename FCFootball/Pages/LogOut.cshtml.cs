using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FCFootball.Pages
{
    public class LogOutModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Clear all session data
            HttpContext.Session.Clear();

            // Optional: TempData message for feedback
            TempData["Message"] = "You have been logged out.";

            // Redirect to home or login
            return RedirectToPage("/Home");
        }
    }
}
