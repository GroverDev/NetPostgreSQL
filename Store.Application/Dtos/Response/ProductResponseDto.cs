namespace Store.Application.Dtos.Response;

public class ProductResponseDto
{
    public string Id { get; set; } = "";
    public string ProductCode { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }
    public decimal SalePrice { get; set; }

    public string ProviderId { get; set; }
    public int InitialStock { get; set; }
    public int CurrentStock { get; set; }
    public bool IsActive { get; set; }
    public int MinReorderQuantity { get; set; }

    public string ProviderName { get; set; } = "";
}