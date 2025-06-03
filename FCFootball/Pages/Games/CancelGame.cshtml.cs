using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FCFootball.Pages.Games
{
    public class CancelGameModel : PageModel
    {
		public RedirectResult OnGet()
		{
			// Set the message.
			TempData["strMessageColor"] = "Red";
			TempData["strMessage"] = "The operation was cancelled. No data was affected.";
			return Redirect("MaintainGames");
		}
	}
}
