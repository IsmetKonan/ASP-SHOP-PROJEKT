public class Article

    // ARTIKEL nach: ID; BESCHREIBUNG; MENGE; PREIS
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
