using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using FCFootball.Models;

namespace FCFootball.Pages.Staff;

public class DeleteStaffModel : PageModel
{

    private readonly FCFootballContext FCFootballContext;
    public DeleteStaffModel(FCFootballContext SPC)
    {
        FCFootballContext = SPC;
    }

    public FCFootball.Models.Staff Staff { get; set; }

    public async Task<IActionResult> OnGetAsync(int intStaffID)
    {

        // Look up the row in the table to see if it still exists.
        Staff = await FCFootballContext.Staff.FindAsync(intStaffID);
        if (Staff != null)
        {
            try
            {
                // Delete the row from the table.
                FCFootballContext.Staff.Remove(Staff);
                await FCFootballContext.SaveChangesAsync();
                // Set the message.
                TempData["strMessageColor"] = "Green";
                TempData["strMessage"] = Staff.FirstName + " " + Staff.LastName + " was successfully removed.";
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
                    TempData["strMessage"] = Staff.FirstName + " " + Staff.LastName + " was NOT deleted because it is associated with one or more other objects. To delete this staff member, you must first delete the associated objects.";
                }
                else
                {
                    // A database exception occurred while saving to
                    // the database.
                    // Set the message.
                    TempData["strMessageColor"] = "Red";
                    TempData["strMessage"] = Staff.FirstName + " " + Staff.LastName + " was NOT deleted. Please report this message to...: " + objDbUpdateException.InnerException.Message;
                }
            }
        }
        else
        {
            // Even though someone else deleted the item first, still
            // inform the user that the item was deleted successfully.
            // Set the message.
            TempData["strMessageColor"] = "Green";
            TempData["strMessage"] = "The staff member was successfully removed from the roster.";
        }
        return Redirect("MaintainStaff");

    }

}
