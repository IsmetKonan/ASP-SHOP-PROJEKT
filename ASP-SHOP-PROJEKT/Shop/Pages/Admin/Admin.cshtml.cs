using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class ProductsModel : PageModel
{
    private readonly DatabaseService _db;

    public List<Article> Articles { get; set; } = new();

    [BindProperty]
    public Article NewArticle { get; set; } = new();

    public ProductsModel(DatabaseService db)
    {
        _db = db;
    }

    public IActionResult OnGet()
    {
        if (HttpContext.Session.GetString("IsAdmin") != "True")
            return RedirectToPage("/Login");

        Articles = _db.GetArticles();
        return Page();
    }

    public IActionResult OnPostDeleteArticle(int articleId)
    {
        if (HttpContext.Session.GetString("IsAdmin") != "True")
            return RedirectToPage("/Login");

        _db.DeleteArticle(articleId);
        return RedirectToPage();
    }

    public IActionResult OnPost()
    {
        if (HttpContext.Session.GetString("IsAdmin") != "True")
            return RedirectToPage("/Login");

        _db.CreateArticle(NewArticle);
        return RedirectToPage();
    }
}
