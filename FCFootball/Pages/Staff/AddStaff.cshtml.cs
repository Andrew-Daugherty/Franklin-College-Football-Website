using System.ComponentModel.DataAnnotations.Schema;
using FCFootball.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace FCFootball.Pages.Staff;

[BindProperties]
public class AddStaffModel : PageModel
{

    public string MessageColor;
    public string Message;

    private readonly FCFootballContext FCFootballContext;
	private readonly IWebHostEnvironment IWebHostEnvironment;
	public AddStaffModel(FCFootballContext SPC, IWebHostEnvironment IWHE)
    {
        FCFootballContext = SPC;
		IWebHostEnvironment = IWHE;
	}

	public FCFootball.Models.Staff Staff { get; set; }

	[NotMapped]
	public IFormFile StaffImage { get; set; }



	public void OnGet()
    {

        MessageColor = "Green";
        Message = "Please fill out the information below and click Add Staff.";

    }

    public async Task<IActionResult> OnPostAddAsync()
    {

		if (StaffImage != null)
		{
			MessageColor = "Green";
			Message = "Image received: " + StaffImage.FileName;

			string strImagesPath = Path.Combine(IWebHostEnvironment.WebRootPath, "images");
			string strFileName = Path.GetFileName(StaffImage.FileName);
			string strFilePath = Path.Combine(strImagesPath, strFileName);
			using (var objFileStream = new FileStream(strFilePath, FileMode.Create))


			{
				StaffImage.CopyTo(objFileStream);
			}
			Staff.Image = strFileName; // Save file name to database
		}

		try
        {
            // Add the row to the table.
            FCFootballContext.Staff.Add(Staff);
            await FCFootballContext.SaveChangesAsync();
            // Set the message.
            TempData["strMessageColor"] = "Green";
            TempData["strMessage"] = Staff.StaffID + " was successfully added.";
        }
        catch (DbUpdateException objDbUpdateException)
        {
            // A database exception occurred while saving to the
            // database.
            // Set the message.
            TempData["strMessageColor"] = "Red";
			TempData["strMessage"] = "The staff member was NOT added because the information given was invalid. Please try again.";
			//TempData["strMessage"] = Staff.StaffID + " was NOT added. Please report this message to...: " + objDbUpdateException.InnerException.Message;
        }
        return Redirect("MaintainStaff");

    }

}
