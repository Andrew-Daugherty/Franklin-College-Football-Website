using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using FCFootball.Models;

namespace FCFootball.Pages.Players;

public class DeletePlayerModel : PageModel
{

    private readonly FCFootballContext FCFootballContext;
    public DeletePlayerModel(FCFootballContext SPC)
    {
        FCFootballContext = SPC;
    }

    private Player Player{ get; set; }

    public async Task<IActionResult> OnGetAsync(int intPlayerID)
    {

        // Look up the row in the table to see if it still exists.
        Player = await FCFootballContext.Player.FindAsync(intPlayerID);
        if (Player != null)
        {
            try
            {
                // Delete the row from the table.
                FCFootballContext.Player.Remove(Player);
                await FCFootballContext.SaveChangesAsync();
                // Set the message.
                TempData["strMessageColor"] = "Green";
                TempData["strMessage"] = Player.FirstName + " " + Player.LastName + " was successfully removed from the roster.";
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
                    TempData["strMessage"] = Player.FirstName + " " + Player.LastName + " was NOT deleted because it is associated with one or more other objects. To delete this player, you must first delete the associated objects.";
                }
                else
                {
                    // A database exception occurred while saving to
                    // the database.
                    // Set the message.
                    TempData["strMessageColor"] = "Red";
                    TempData["strMessage"] = Player.FirstName + " " + Player.LastName + " was NOT deleted. Please report this message to...: " + objDbUpdateException.InnerException.Message;
                }
            }
        }
        else
        {
            // Even though someone else deleted the item first, still
            // inform the user that the item was deleted successfully.
            // Set the message.
            TempData["strMessageColor"] = "Green";
            TempData["strMessage"] = "The player was successfully removed from the roster.";
        }
        return Redirect("MaintainPlayers");

    }

}
