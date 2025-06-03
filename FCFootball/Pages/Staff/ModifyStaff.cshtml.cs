using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FCFootball.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace FCFootball.Pages.Staff;

[BindProperties]
public class ModifyStaffModel : PageModel
{

	public string MessageColor;
	public string Message;

	private readonly FCFootballContext FCFootballContext;
	private readonly IWebHostEnvironment IWebHostEnvironment;
	public ModifyStaffModel(FCFootballContext SPC, IWebHostEnvironment IWHE)
	{
		FCFootballContext = SPC;
		IWebHostEnvironment = IWHE;
	}

	public FCFootball.Models.Staff Staff { get; set; }
	[NotMapped]
	public IFormFile StaffImage { get; set; }
	public string OldStaffImage { get; set; }
	public string OldPassword { get; set; }

	public async Task<IActionResult> OnGetAsync(int intStaffID)
	{

		// Set the message.
		MessageColor = "Green";
		Message = "Please modify the information below and click Modify Staff Member.";

		Staff = await FCFootballContext.Staff.FindAsync(intStaffID);
		OldStaffImage = Staff.Image;
		OldPassword = Staff.Password;

		return Page();
	}

	public async Task<IActionResult> OnPostModifyAsync()
	{

		if (StaffImage != null)
		{
			//MessageColor = "Green";
			//Message = "Image received: " + StaffImage.FileName;

			string strImagesPath = Path.Combine(IWebHostEnvironment.WebRootPath, "images");
			string strFileName = Path.GetFileName(StaffImage.FileName);
			string strFilePath = Path.Combine(strImagesPath, strFileName);
			using (var objFileStream = new FileStream(strFilePath, FileMode.Create))


			{
				StaffImage.CopyTo(objFileStream);
			}
			Staff.Image = strFileName; // Save file name to database
		}

		else
		{
			Staff.Image = OldStaffImage;
		}

		if (string.IsNullOrWhiteSpace(Staff.Password))
		{
			Staff.Password = OldPassword;
		}

		try
		{
			// Modify the row in the table.
			FCFootballContext.Staff.Update(Staff);
			await FCFootballContext.SaveChangesAsync();
			// Set the message.
			TempData["strMessageColor"] = "Green";
			TempData["strMessage"] = "Staff member was successfully modified.";
		}
		catch (DbUpdateException objDbUpdateException)
		{
			// A database exception occurred while saving to the
			// database.
			// Set the message.
			TempData["strMessageColor"] = "Red";
			TempData["strMessage"] = "The selected staff member was NOT modified because the information given was invalid. Please try again.";
			//TempData["strMessage"] = "Staff member was NOT modified. Please report this message to...: " + objDbUpdateException.InnerException.Message;

		}
		return Redirect("MaintainStaff");

	}

}
