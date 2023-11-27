using AutoMapper;
using Common.Utilities;
using Microsoft.AspNetCore.ResponseCompression;
using Store.Application.Dtos.Response;
using Store.Application.Interfaces;
using Store.Application.Validators.Product;
using Store.Domain;

namespace Store.Application.Services;

public class ProductApplication : IProductApplication
{
    private readonly IMapper _mapper;
    private readonly ProductValidator _validationRules;
    public ProductApplication(IMapper mapper, ProductValidator validationRules)
    {
        _mapper = mapper;
        _validationRules = validationRules;
    }

    public async Task<BaseResponse<bool>> CreateProduct(ProductRequestDto requestDto)
    {
        var response = new BaseResponse<bool>();
        var validationResult = await _validationRules.ValidateAsync(requestDto);

        if(validationResult.IsValid)
        {
            response.Ok = false;
            response.Message = "Datos no validos";
            //response.Errors = validationResult.Errors;
        }

        var product = _mapper.Map<Product>(requestDto);
        response = null;
        // if(response.Data)
        // {
            
        // }

        return response;
    }

    public async Task<BaseResponse<IEnumerable<ProductResponseDto>>> ListProducts()
    {
        var response = new BaseResponse<IEnumerable<ProductResponseDto>>();

        response.Ok = true;
        response.Data = _mapper.Map<IEnumerable<ProductResponseDto>>(null);
        response.Message = "Ok";

        return response;
    }
}
