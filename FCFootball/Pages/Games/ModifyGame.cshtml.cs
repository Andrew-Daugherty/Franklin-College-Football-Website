using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FCFootball.Models;

namespace FCFootball.Pages.Games;

[BindProperties]
public class ModifyGameModel : PageModel
{

	public string MessageColor;
	public string Message;

	private readonly FCFootballContext FCFootballContext;
	public ModifyGameModel(FCFootballContext SPC)
	{
		FCFootballContext = SPC;
	}

	public FCFootball.Models.Game Game { get; set; }

	public async Task<IActionResult> OnGetAsync(int intGameID)
	{

		// Set the message.
		MessageColor = "Green";
		Message = "Please modify the information below and click Modify Game.";

		Game = await FCFootballContext.Game.FindAsync(intGameID);

		return Page();

	}

	public async Task<IActionResult> OnPostModifyAsync()
	{

		try
		{
			// Modify the row in the table.
			FCFootballContext.Game.Update(Game);
			await FCFootballContext.SaveChangesAsync();
			// Set the message.
			TempData["strMessageColor"] = "Green";
			TempData["strMessage"] = "Game was successfully modified.";
		}
		catch (DbUpdateException objDbUpdateException)
		{
			// A database exception occurred while saving to the
			// database.
			// Set the message.
			TempData["strMessageColor"] = "Red";
			TempData["strMessage"] = "The selected game was NOT modified because the information given was invalid. Please try again.";
			//TempData["strMessage"] = "Game was NOT modified. Please report this message to...: " + objDbUpdateException.InnerException.Message;
		}
		return Redirect("MaintainGames");

	}

}
