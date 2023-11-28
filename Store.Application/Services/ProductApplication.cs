using AutoMapper;
using Common.Utilities.Bases;
using Store.Application.Dtos.Response;
using Store.Application.Interfaces;
using Store.Application.Validators.Product;
using Store.Domain;
using Store.Infrastructure;
using Store.Infrastructure.Interfaces;

namespace Store.Application.Services;

public class ProductApplication : IProductApplication
{
    //private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ProductValidator _validationRules;
    public ProductApplication(IProductRepository productRepository, IMapper mapper, ProductValidator validationRules)
    {
        //_unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _mapper = mapper;
        _validationRules = validationRules;
    }

    public async Task<BaseResponse<bool>> CreateProduct(ProductRequestDto requestDto)
    {
        var response = new BaseResponse<bool>();
        var validationResult = await _validationRules.ValidateAsync(requestDto);

        if (validationResult.IsValid)
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
        try
        {
            var products = await _productRepository.GetAllProductsAsync();
            if (products is not null)
            {
                response.Ok = true;
                response.Data = _mapper.Map<IEnumerable<ProductResponseDto>>(products);
                response.Message = "Todo ok";
            }
        }
        catch (System.Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }
}
