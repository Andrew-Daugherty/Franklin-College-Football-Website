using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FCFootball.Models;

namespace FCFootball.Pages.Players;

public class MaintainPlayersModel : PageModel
{
    public string MessageColor;
    public string Message;

    private readonly FCFootballContext FCFootballContext;
    public MaintainPlayersModel(FCFootballContext SPC)
    {
        FCFootballContext = SPC;
    }

    public class Result
    {
        public string? FirstName;
        public string? LastName;
        public byte? Number;
        public int? PlayerID;
        public string? Image;
        public string? Height;
        public short? Weight;
        public string? Position;
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
            from p in FCFootballContext.Player
            orderby p.Number
            select new Result
            {
                Image = p.Image,
                Number = p.Number,
                FirstName = p.FirstName,
                LastName = p.LastName,
                PlayerID = p.PlayerID,
                Height = p.Height,
                Weight = p.Weight,
                Position = p.Position
            });
        // Retrieve the rows for display.
        ResultIList = await ResultIQueryable.ToListAsync();
    }
}
