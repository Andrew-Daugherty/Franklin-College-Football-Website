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
public class AddPlayerStatModel : PageModel
{

	public string MessageColor;
	public string Message;

	private readonly FCFootballContext FCFootballContext;

	public AddPlayerStatModel(FCFootballContext SPC)
	{
		FCFootballContext = SPC;
	}

	public SelectList OpponentSelectList;
	public SelectList PlayerSelectList;

	public int? GameID { get; set; }
	public FCFootball.Models.PlayerStat PlayerStat { get; set; }



	public void OnGet()
	{

		MessageColor = "Green";
		Message = "Please fill out the information below and click Add Statistics.";

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


	}

	public async Task<IActionResult> OnPostAddAsync()
	{

		try
		{
			// Add the row to the table.
			FCFootballContext.PlayerStats.Add(PlayerStat);
			await FCFootballContext.SaveChangesAsync();
			// Set the message.
			TempData["strMessageColor"] = "Green";
			TempData["strMessage"] = "The statistics were successfully added.";
		}
		catch (DbUpdateException objDbUpdateException)
		{
			// A database exception occurred while saving to the
			// database.
			// Set the message.
			TempData["strMessageColor"] = "Red";
			TempData["strMessage"] = "The player statistics were NOT added because the information given was invalid.  Please try again.";
			//TempData["strMessage"] = "The statistics were NOT added. Please report this message to...: " + objDbUpdateException.InnerException.Message;
		}
		return Redirect("MaintainPlayerStats");

	}

}
