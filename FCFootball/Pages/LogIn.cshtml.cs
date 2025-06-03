using FCFootball.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FCFootball.Pages;

[BindProperties]
public class LogInModel : PageModel
{

	private readonly FCFootball.Models.FCFootballContext FCFootballContext;
	public LogInModel(FCFootball.Models.FCFootballContext SPC)
	{
		FCFootballContext = SPC;
	}

	public string MessageColor;
	public string Message;

	public string EmailAddress { get; set; }
	public string Password { get; set; }

	private FCFootball.Models.Staff Staff;

	public void OnGet()
	{
	}

    public async Task OnPostLogIn()
    {
        // Log in the user.
        Staff = await FCFootballContext.Staff
            .Where(s => s.EmailAddress == EmailAddress && s.Password == Password)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (Staff != null)
        {
            // Store session values
            HttpContext.Session.SetString("LoggedIn", "true");
            HttpContext.Session.SetString("StaffName", Staff.FirstName + " " + Staff.LastName);

            MessageColor = "Green";
            Message = "Welcome " + Staff.FirstName + " " + Staff.LastName + "!";
        }
        else
        {
            MessageColor = "Red";
            Message = "You have entered an invalid email address and password combination. Please try again.";
        }
    }

}