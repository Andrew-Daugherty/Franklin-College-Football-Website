using FCFootball.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;


namespace FCFootball.Pages.PlayerStats;

[BindProperties]

public class ModifyPlayerStatModel : PageModel
{
	public string MessageColor;
	public string Message;

	private readonly FCFootballContext FCFootballContext;
	public ModifyPlayerStatModel(FCFootballContext SPC)
	{
		FCFootballContext = SPC;
	}
	public SelectList OpponentSelectList;
	public SelectList PlayerSelectList;

	public int? GameID { get; set; }
	public FCFootball.Models.PlayerStat PlayerStat { get; set; }

	public async Task<IActionResult> OnGetAsync(int intPlayerStatID)
	{

		// Set the message.
		MessageColor = "Green";
		Message = "Please modify the information below and click Modify Statistics.";

		PlayerStat = await FCFootballContext.PlayerStats.FindAsync(intPlayerStatID);
		if (PlayerStat != null)
		{
			// Populate the opponent select list.
			OpponentSelectList = new SelectList(FCFootballContext.Game.
				OrderBy(g => g.Opponent).
				Select(g => new { g.GameID, g.Opponent }),
				"GameID", "Opponent");

			// Populate the opponent select list.
			PlayerSelectList = new SelectList(FCFootballContext.Player.
				OrderBy(p => p.LastName).
				Select(p => new { p.PlayerID, FullName = p.FirstName + " " + p.LastName }),
				"PlayerID", "FullName");

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
			FCFootballContext.PlayerStats.Update(PlayerStat);
			await FCFootballContext.SaveChangesAsync();
			// Set the message.
			TempData["strMessageColor"] = "Green";
			TempData["strMessage"] = "Player statistics were successfully modified.";
		}
		catch (DbUpdateException objDbUpdateException)
		{
			// A database exception occurred while saving to the
			// database.
			// Set the message.
			TempData["strMessageColor"] = "Red";
			TempData["strMessage"] = "The selected player statistics were NOT modified because the information given was invalid.  Please try again.";
			//TempData["strMessage"] = "Game statistic was NOT modified. Please report this message to...: " + objDbUpdateException.InnerException.Message;
		}
		return Redirect("MaintainPlayerStats");

	}

}
