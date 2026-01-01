using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class LoginModel : PageModel
{
    private readonly DatabaseService _db;

    [BindProperty] public string Username { get; set; } = string.Empty;
    [BindProperty] public string Password { get; set; } = string.Empty;

    public LoginModel(DatabaseService db)
    {
        _db = db;
    }

    public IActionResult OnPost()
    {
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
        {
            ModelState.AddModelError(string.Empty, "Bitte Benutzername und Passwort eingeben.");
            return Page();
        }

        var hash = PasswordHelper.Hash(Password);

        var user = _db.GetUserByLogin(Username, hash);

        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Benutzername oder Passwort falsch.");
            return Page();
        }

        HttpContext.Session.SetInt32("UserId", user.Id);
        HttpContext.Session.SetString("Username", user.Username);
        HttpContext.Session.SetString("IsAdmin", user.IsAdmin.ToString());

        return RedirectToPage("/Index");
    }

}
