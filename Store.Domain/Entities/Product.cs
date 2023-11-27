namespace Store.Domain;

public class Product : Audit
{
    public Guid Id { get; set; }
    public string ProductCode { get; set; }
    public string ProductName { get; set; }

    public string Description { get; set; }
    public decimal SalePrice { get; set; }
    public Guid ProviderId { get; set; }

    public int InitialStock { get; set; }
    public int CurrentStock { get; set; }
    public bool IsActive { get; set; }
    public int MinReorderQuantity { get; set; }

    // Provider
    public string ProviderName { get; set; } = "";

}
