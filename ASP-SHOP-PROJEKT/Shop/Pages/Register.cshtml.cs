using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class RegisterModel : PageModel
{
    private readonly DatabaseService _db;

    [BindProperty]
    public User NewUser { get; set; } = new();

    [BindProperty]
    public string Password { get; set; } = string.Empty;

    public RegisterModel(DatabaseService db)
    {
        _db = db;
    }

    public IActionResult OnPost()
    {
        if (string.IsNullOrWhiteSpace(NewUser.Username) ||
            string.IsNullOrWhiteSpace(Password) ||
            string.IsNullOrWhiteSpace(NewUser.FirstName) ||
            string.IsNullOrWhiteSpace(NewUser.LastName) ||
            string.IsNullOrWhiteSpace(NewUser.Email))
        {
            ModelState.AddModelError(string.Empty, "Bitte alle Pflichtfelder ausfüllen.");
            return Page();
        }

        NewUser.PasswordHash = PasswordHelper.Hash(Password);

        _db.CreateUser(NewUser);

        return RedirectToPage("/Login");
    }
}
