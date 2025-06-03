using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FCFootball.Models;
using System.ComponentModel.DataAnnotations.Schema;



namespace FCFootball.Pages.Players;

[BindProperties]
public class ModifyPlayerModel : PageModel
{

	public string MessageColor;
	public string Message;

	private readonly FCFootballContext FCFootballContext;
    private readonly IWebHostEnvironment IWebHostEnvironment;
    public ModifyPlayerModel(FCFootballContext SPC, IWebHostEnvironment IWHE)
	{
		FCFootballContext = SPC;
        IWebHostEnvironment = IWHE;
    }

	public FCFootball.Models.Player Player { get; set; }

    [NotMapped]
    public IFormFile PlayerImage { get; set; }
	public string OldPlayerImage { get; set; }

    public async Task<IActionResult> OnGetAsync(int intPlayerID)
	{

		// Set the message.
		MessageColor = "Green";
		Message = "Please modify the information below and click Modify Player.";

		Player = await FCFootballContext.Player.FindAsync(intPlayerID);
		OldPlayerImage = Player.Image;

		return Page();
	}

	public async Task<IActionResult> OnPostModifyAsync()
	{
		if (PlayerImage != null)
		{
			//MessageColor = "Green";

			string strImagesPath = Path.Combine(IWebHostEnvironment.WebRootPath, "images");
			string strFileName = Path.GetFileName(PlayerImage.FileName);
			string strFilePath = Path.Combine(strImagesPath, strFileName);
			using (var objFileStream = new FileStream(strFilePath, FileMode.Create))

			{
				PlayerImage.CopyTo(objFileStream);
			}
			Player.Image = strFileName; // Save file name to database
		}

		else 
		{

			Player.Image = OldPlayerImage;

		}

        try
		{
			// Modify the row in the table.
			FCFootballContext.Player.Update(Player);
			await FCFootballContext.SaveChangesAsync();
			// Set the message.
			TempData["strMessageColor"] = "Green";
			TempData["strMessage"] = "Player was successfully modified.";
		}
		catch (DbUpdateException objDbUpdateException)
		{
			// A database exception occurred while saving to the
			// database.
			// Set the message.
			TempData["strMessageColor"] = "Red";
			TempData["strMessage"] = "The selected player was NOT modified because the information given was invalid. Please try again.";
			//TempData["strMessage"] = "Player was NOT modified. Please report this message to...: " + objDbUpdateException.InnerException.Message;
		}
		return Redirect("MaintainPlayers");

	}

}
