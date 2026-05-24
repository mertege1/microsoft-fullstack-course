namespace InventoryHub.Client.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = "General";
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}