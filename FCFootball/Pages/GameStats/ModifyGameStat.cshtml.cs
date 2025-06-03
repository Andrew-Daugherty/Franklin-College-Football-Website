using FCFootball.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;


namespace FCFootball.Pages.GameStats;

[BindProperties]

public class ModifyGameStatModel : PageModel
{
	public string MessageColor;
	public string Message;

	private readonly FCFootballContext FCFootballContext;
	public ModifyGameStatModel(FCFootballContext SPC)
	{
		FCFootballContext = SPC;
	}
	public SelectList OpponentSelectList;

	public int? GameID { get; set; }
	public FCFootball.Models.GameStat GameStat { get; set; }

	public async Task<IActionResult> OnGetAsync(int intGameStatID)
	{

		// Set the message.
		MessageColor = "Green";
		Message = "Please modify the information below and click Modify Statistics.";

		GameStat = await FCFootballContext.GameStats.FindAsync(intGameStatID);
		if (GameStat != null)
		{
			/// Populate the opponent select list.
			OpponentSelectList = new SelectList(FCFootballContext.Game.
				OrderBy(g => g.Opponent).
				Select(g => new { g.GameID, g.Opponent }),
				"GameID", "Opponent");
			return Page();
		}
		else
		{
			// Set the message.
			TempData["strMessageColor"] = "Red";
			TempData["strMessage"] = "The selected statistics were already deleted.";
			return Redirect("MaintainGameStats");
		}
	}

	public async Task<IActionResult> OnPostModifyAsync()
	{

		try
		{
			// Modify the row in the table.
			FCFootballContext.GameStats.Update(GameStat);
			await FCFootballContext.SaveChangesAsync();
			// Set the message.
			TempData["strMessageColor"] = "Green";
			TempData["strMessage"] = "Game statistic was successfully modified.";
		}
		catch (DbUpdateException objDbUpdateException)
		{
			// A database exception occurred while saving to the
			// database.
			// Set the message.
			TempData["strMessageColor"] = "Red";
			TempData["strMessage"] = "The selected game statistics were NOT modified because the information given was invalid. Please try again.";
			//TempData["strMessage"] = "Game statistic was NOT modified. Please report this message to...: " + objDbUpdateException.InnerException.Message;
		}
		return Redirect("MaintainGameStats");

	}

}
