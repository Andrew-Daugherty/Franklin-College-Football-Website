using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FCFootball.Models;

namespace FCFootball.Pages.Staff;

public class MaintainStaffModel : PageModel
{
    public string MessageColor;
    public string Message;

    private readonly FCFootballContext FCFootballContext;
    public MaintainStaffModel(FCFootballContext SPC)
    {
        FCFootballContext = SPC;
    }

    public class Result
    {
        public string? FirstName;
        public string? LastName;
        public int? StaffID;
        public string? Image;
        public string? EmailAddress;
        public string? Role;
    }

    private IQueryable<Result> ResultIQueryable;
    public IList<Result> ResultIList;

    public async Task OnGetAsync()
    {

        // Set the message.
        if (TempData["strMessage"] == null)
        {
            TempData["strMessageColor"] = "Green";
            TempData["strMessage"] = "Please choose an option below.";
        }
        else
        {
            MessageColor = TempData["strMessageColor"].ToString();
            Message = TempData["strMessage"].ToString();
        }

        // Define the database query.
        ResultIQueryable = (
            from s in FCFootballContext.Staff
            orderby s.LastName
            select new Result
            {
                LastName = s.LastName,
                FirstName = s.FirstName,
                StaffID = s.StaffID,
                Image = s.Image,
                EmailAddress = s.EmailAddress,
                Role = s.Role
            });
        // Retrieve the rows for display.
        ResultIList = await ResultIQueryable.ToListAsync();
    }
}
