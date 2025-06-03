using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using FCFootball.Models;

namespace FCFootball.Pages.GameStats;

public class DeleteGameStatModel : PageModel
{

	private readonly FCFootballContext FCFootballContext;
	public DeleteGameStatModel(FCFootballContext SPC)
	{
		FCFootballContext = SPC;
	}

	private GameStat GameStat { get; set; }

	public async Task<IActionResult> OnGetAsync(int intGameStatID)
	{

		// Look up the row in the table to see if it still exists.
		GameStat = await FCFootballContext.GameStats.FindAsync(intGameStatID);
		if (GameStat != null)
		{
			try
			{
				// Delete the row from the table.
				FCFootballContext.GameStats.Remove(GameStat);
				await FCFootballContext.SaveChangesAsync();
				// Set the message.
				TempData["strMessageColor"] = "Green";
				TempData["strMessage"] = "The selected statistics were successfully removed.";
			}
			catch (DbUpdateException objDbUpdateException)
			{
				// A database exception occurred.
				SqlException objSqlException = objDbUpdateException.InnerException as SqlException;
				if (objSqlException.Number == 547)
				{
					// A foreign key constraint database exception
					// occurred.
					// Set the message.
					TempData["strMessageColor"] = "Red";
					TempData["strMessage"] = "The selected statistics were NOT deleted because it is associated with one or more other objects. To delete these statistics, you must first delete the associated objects.";
				}
				else
				{
					// A database exception occurred while saving to
					// the database.
					// Set the message.
					TempData["strMessageColor"] = "Red";
					TempData["strMessage"] = "The selected statistics were NOT deleted. Please report this message to...: " + objDbUpdateException.InnerException.Message;
				}
			}
		}
		else
		{
			// Even though someone else deleted the item first, still
			// inform the user that the item was deleted successfully.
			// Set the message.
			TempData["strMessageColor"] = "Green";
			TempData["strMessage"] = "The selected statistics were successfully removed.";
		}
		return RedirectToPage("/GameStats/MaintainGameStats");

    }

}
