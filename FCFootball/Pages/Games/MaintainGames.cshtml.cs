using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FCFootball.Models;

namespace FCFootball.Pages.Games;

public class MaintainGamesModel : PageModel
{
    public string MessageColor;
    public string Message;

    private readonly FCFootballContext FCFootballContext;
    public MaintainGamesModel(FCFootballContext SPC)
    {
        FCFootballContext = SPC;
    }

    public class GameResult
    {
        public string? Opponent;
        public DateTime? Date;
        public bool? Home;
        public int? GameID;
        public byte? TeamScore;
        public byte? OpponentScore;
        public string? Result;
    }

    private IQueryable<GameResult> ResultIQueryable;
    public IList<GameResult> ResultIList;

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
            from g in FCFootballContext.Game
            orderby g.Date
            select new GameResult
            {
                Opponent = g.Opponent,
                Date = g.Date,
                Home = g.Home,
                GameID = g.GameID,
                TeamScore = g.TeamScore,
                OpponentScore = g.OpponentScore,
                Result = g.Result
            });
        // Retrieve the rows for display.
        ResultIList = await ResultIQueryable.ToListAsync();

    }
}
