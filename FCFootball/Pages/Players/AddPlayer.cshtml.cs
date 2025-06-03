using FCFootball.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;


namespace FCFootball.Pages.Players;

[BindProperties]
public class AddPlayerModel : PageModel
{

    public string MessageColor;
	public string Message;

	private readonly FCFootballContext FCFootballContext;
	private readonly IWebHostEnvironment IWebHostEnvironment;
	public AddPlayerModel(FCFootballContext SPC, IWebHostEnvironment IWHE)
	{
		FCFootballContext = SPC;
		IWebHostEnvironment = IWHE;
	}

	public FCFootball.Models.Player Player { get; set; }

	[NotMapped]
	public IFormFile PlayerImage { get; set; }


    public void OnGet()
        {

		MessageColor = "Green";
		Message = "Please fill out the information below and click Add Player to Roster.";

		}

	public async Task<IActionResult> OnPostAddAsync()
	{
		if (PlayerImage != null)
		{
			MessageColor = "Green";
			Message = "Image received: " + PlayerImage.FileName;

			string strImagesPath = Path.Combine(IWebHostEnvironment.WebRootPath, "images");
			string strFileName = Path.GetFileName(PlayerImage.FileName);
			string strFilePath = Path.Combine(strImagesPath, strFileName);
			using (var objFileStream = new FileStream(strFilePath, FileMode.Create))


			{
				PlayerImage.CopyTo(objFileStream);
			}
			Player.Image = strFileName; // Save file name to database
		}

		try
		{
			// Add the row to the table.
			FCFootballContext.Player.Add(Player);
			await FCFootballContext.SaveChangesAsync();
			// Set the message.
			TempData["strMessageColor"] = "Green";
			TempData["strMessage"] = Player.FirstName + " " + Player.LastName + " was successfully added to the roster.";
		}
		catch (DbUpdateException objDbUpdateException)
		{
			// A database exception occurred while saving to the
			// database.
			// Set the message.
			TempData["strMessageColor"] = "Red";
			TempData["strMessage"] = "The player was NOT added because the information given was invalid. Please try again.";
			//TempData["strMessage"] = "The player was NOT added. Please report this message to...: " + objDbUpdateException.InnerException.Message;
		}
		return Redirect("MaintainPlayers");

	}

}
