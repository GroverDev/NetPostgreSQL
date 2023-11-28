using Microsoft.AspNetCore.Mvc;
using Store.Application.Interfaces;

namespace Store.Api;

[Route("api/[controller]")]
[ApiController]
public class ProductController: ControllerBase
{
    private readonly IProductApplication _productApplication;

    public ProductController(IProductApplication productApplication)
    {
        _productApplication = productApplication;
    }
    [HttpGet]
    public async Task<IActionResult> ListProducts()
    {
        var response = await _productApplication.ListProducts();
        return Ok(response);
    }

}
