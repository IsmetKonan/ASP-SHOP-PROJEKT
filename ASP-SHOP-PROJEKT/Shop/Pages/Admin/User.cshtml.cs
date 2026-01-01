using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class UsersModel : PageModel
{
    private readonly DatabaseService _db;

    public List<User> Users { get; set; } = new();

    [BindProperty]
    public NewUserInput NewUser { get; set; } = new();

    public UsersModel(DatabaseService db)
    {
        _db = db;
    }

    public IActionResult OnGet()
    {
        if (HttpContext.Session.GetString("IsAdmin") != "True")
            return RedirectToPage("/Login");

        Users = _db.GetAllUsers();
        return Page();
    }

    public IActionResult OnPost()
    {
        if (HttpContext.Session.GetString("IsAdmin") != "True")
            return RedirectToPage("/Login");

        var user = new User
        {
            Username = NewUser.Username,
            PasswordHash = PasswordHelper.Hash(NewUser.Password),
            FirstName = NewUser.FirstName,
            LastName = NewUser.LastName,
            Email = NewUser.Email,
            Phone = NewUser.Phone,
            IsAdmin = false
        };

        _db.CreateUser(user);
        return RedirectToPage();
    }

    public IActionResult OnPostToggleAdmin(int userId)
    {
        if (HttpContext.Session.GetString("IsAdmin") != "True")
            return RedirectToPage("/Login");

        _db.ToggleAdmin(userId);
        return RedirectToPage();
    }

    public IActionResult OnPostDeleteUser(int userId)
    {
        if (HttpContext.Session.GetString("IsAdmin") != "True")
            return RedirectToPage("/Login");

        _db.DeleteUser(userId);
        return RedirectToPage();
    }

    public class NewUserInput
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
