using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FCFootball.Models;

namespace FCFootball.Pages.GameStats;

public class MaintainGameStatsModel : PageModel
{
    public string MessageColor;
    public string Message;

    private readonly FCFootballContext FCFootballContext;
    public MaintainGameStatsModel(FCFootballContext SPC)
    {
        FCFootballContext = SPC;
    }

    public class Result
    {
        public int? GameStatID;
        public int? GameID;
        public short? PassAtt;
        public short? PassComp;
        public short? PassYds;
        public short? PassTd;
        public short? RushAtt;
        public short? RushYds;
        public short? RushTd;
        public short? RecYds;
        public short? RecTd;
        public string? Opponent;
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
            from gs in FCFootballContext.GameStats
            join g in FCFootballContext.Game on gs.GameID equals g.GameID
            orderby gs.GameStatID
            select new Result
            {
                GameStatID = gs.GameStatID,
                PassAtt = gs.PassAtt,
                PassComp = gs.PassComp,
                PassYds = gs.PassYds,
                PassTd = gs.PassTd,
                RushAtt = gs.RushAtt,
                RushYds = gs.RushYds,
                RushTd = gs.RushTd,
                RecYds = gs.RecYds,
                RecTd = gs.RecTd,
                Opponent = g.Opponent
            });
        // Retrieve the rows for display.
        ResultIList = await ResultIQueryable.ToListAsync();
    }
}
