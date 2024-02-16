namespace ICommerce.Catalog.Models;
public class Product
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }

    public DateTime ManufacturedAt { get; set; }
    public Product(Guid id, string title, string description, DateTime createdAt,DateTime lastUpdatedAt, DateTime manufacturedAt)
    {
        Id=id;
        Title=title;
        Description=description;
        CreatedAt=createdAt;
        LastUpdatedAt=lastUpdatedAt;
        ManufacturedAt=manufacturedAt;
    }
}
