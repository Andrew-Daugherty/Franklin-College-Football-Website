using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FCFootball.Models;

namespace FCFootball.Pages.PlayerStats;

public class MaintainPlayerStatsModel : PageModel
{
    public string MessageColor;
    public string Message;

    private readonly FCFootballContext FCFootballContext;
    public MaintainPlayerStatsModel(FCFootballContext SPC)
    {
        FCFootballContext = SPC;
    }

    public class Result
    {
        public int? PlayerStatID;
        public int? PlayerID;
        public string? FirstName;
        public string? LastName;
        public string? Image;
        public byte? Number;
        public short? PassAtt;
        public short? PassComp;
        public short? PassYds;
        public short? PassTd;
        public short? RushAtt;
        public short? RushYds;
        public short? RushTd;
        public short? Receptions;
        public short? RecYds;
        public short? RecTd;
        public string? Opponent;
        public int? GameID;
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
            from ps in FCFootballContext.PlayerStats
            join g in FCFootballContext.Game on ps.GameID equals g.GameID
            join p in FCFootballContext.Player on ps.PlayerID equals p.PlayerID
            orderby ps.PlayerStatID
            select new Result
            {
                PlayerStatID = ps.PlayerStatID,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Image = p.Image,
                Number = p.Number,
                PassAtt = ps.PassAtt,
                PassComp = ps.PassComp,
                PassYds = ps.PassYds,
                PassTd = ps.PassTd,
                RushAtt = ps.RushAtt,
                RushYds = ps.RushYds,
                RushTd = ps.RushTd,
                Receptions = ps.Receptions,
                RecYds = ps.RecYds,
                RecTd = ps.RecTd,
                GameID = g.GameID,
                Opponent = g.Opponent
            });
        // Retrieve the rows for display.
        ResultIList = await ResultIQueryable.ToListAsync();
    }
}
