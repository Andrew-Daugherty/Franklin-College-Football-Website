using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using FCFootball.Models;

namespace FCFootball.Pages.Games;

public class DeleteGameModel : PageModel
{

    private readonly FCFootballContext FCFootballContext;
    public DeleteGameModel(FCFootballContext SPC)
    {
        FCFootballContext = SPC;
    }

    private Game Game { get; set; }

    public async Task<IActionResult> OnGetAsync(int intGameID)
    {

        // Look up the row in the table to see if it still exists.
        Game = await FCFootballContext.Game.FindAsync(intGameID);
        if (Game != null)
        {
            try
            {
                // Delete the row from the table.
                FCFootballContext.Game.Remove(Game);
                await FCFootballContext.SaveChangesAsync();
                // Set the message.
                TempData["strMessageColor"] = "Green";
                TempData["strMessage"] = Game.Opponent + " was successfully removed from the schedule.";
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
                    TempData["strMessage"] = Game.Opponent + " was NOT removed from the schedule because it is associated with one or more other objects. To remove this game, you must first delete the associated objects.";
                }
                else
                {
                    // A database exception occurred while saving to
                    // the database.
                    // Set the message.
                    TempData["strMessageColor"] = "Red";
                    TempData["strMessage"] = Game.Opponent + " was NOT removed from the schedule. Please report this message to...: " + objDbUpdateException.InnerException.Message;
                }
            }
        }
        else
        {
            // Even though someone else deleted the item first, still
            // inform the user that the item was deleted successfully.
            // Set the message.
            TempData["strMessageColor"] = "Green";
            TempData["strMessage"] = "The game was successfully removed from the schedule.";
        }
        return Redirect("MaintainGames");

    }

}
