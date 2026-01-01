using Microsoft.AspNetCore.Mvc.RazorPages;

public class IndexModel : PageModel
{
    private readonly DatabaseService _db;

    public List<Article> Articles { get; set; } = new();

    public IndexModel(DatabaseService db)
    {
        _db = db;
    }

    public void OnGet()
    {
        Articles = _db.GetArticles();
    }
}
