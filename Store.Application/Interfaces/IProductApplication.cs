using Common.Utilities;
using Store.Application.Dtos.Response;

namespace Store.Application.Interfaces;

public interface IProductApplication
{
    Task<BaseResponse<IEnumerable<ProductResponseDto>>> ListProducts();

    Task<BaseResponse<bool>> CreateProduct(ProductRequestDto requestDto);
}
