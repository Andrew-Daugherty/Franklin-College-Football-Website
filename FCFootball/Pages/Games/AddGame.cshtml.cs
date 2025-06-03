using FCFootball.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace FCFootball.Pages;

[BindProperties]
public class AddGameModel : PageModel
{

	public string MessageColor;
	public string Message;

	private readonly FCFootballContext FCFootballContext;
	public AddGameModel(FCFootballContext SPC)
	{
		FCFootballContext = SPC;
	}

	public Game Game { get; set; }


	public void OnGet()
	{

		MessageColor = "Green";
		Message = "Please fill out the information below and click Add Game to Schedule.";

	}

	public async Task<IActionResult> OnPostAddAsync()
	{

		try
		{
			// Add the row to the table.
			FCFootballContext.Game.Add(Game);
			await FCFootballContext.SaveChangesAsync();
			// Set the message.
			TempData["strMessageColor"] = "Green";
			TempData["strMessage"] = Game.Opponent + " was successfully added to the schedule.";
		}
		catch (DbUpdateException objDbUpdateException)
		{
			// A database exception occurred while saving to the
			// database.
			// Set the message.
			TempData["strMessageColor"] = "Red";
			TempData["strMessage"] = "The game was NOT added because the information given was invalid. Please try again.";
			//TempData["strMessage"] = Game.Opponent + " was NOT added. Please report this message to...: " + objDbUpdateException.InnerException.Message;
		}
		return Redirect("MaintainGames");

	}

}
