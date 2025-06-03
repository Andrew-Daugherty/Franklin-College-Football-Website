using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FCFootball.Pages.Players
{
    public class CancelPlayerModel : PageModel
    {
        public RedirectResult OnGet()
        {
			// Set the message.
			TempData["strMessageColor"] = "Red";
			TempData["strMessage"] = "The operation was cancelled. No data was affected.";
			return Redirect("MaintainPlayers");
		}
    }
}
