using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;
using AutoMapper;
using API.Errors;
using API.Helpers;

namespace API.Controllers
{
  public class ProductsController : BaseApiController
    {
    private readonly IMapper _mapper;
    
    private readonly IGenericRepository<Product> _productsRepo;
    private readonly IGenericRepository<ProductType> _productsTypeRepo;
    private readonly IGenericRepository<ProductBrand> _productsBrandRepo;
    public ProductsController(IGenericRepository<Product> productsRepo,IGenericRepository<ProductBrand> productsBrandRepo, IGenericRepository<ProductType> productsTypeRepo, IMapper mapper)
    {
      _mapper = mapper;
      _productsTypeRepo = productsTypeRepo;
      _productsRepo = productsRepo;
      _productsBrandRepo = productsBrandRepo;
    }

    [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams productsParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productsParams);
            var countSpec = new ProductsWithFiltersForCountSpecification(productsParams);
            var totalItems = await _productsRepo.CountAsync(countSpec);
            var products = await _productsRepo.ListAsync(spec);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
            return Ok(new Pagination<ProductToReturnDto>(productsParams.PageIndex, productsParams.PageSize,totalItems,data));
    }

      [HttpGet("{id}")]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product =  await _productsRepo.GetEntityWithSpec(spec);
            if(product == null){
              return NotFound(new ApiResponse(404));
            }

      return _mapper.Map<Product, ProductToReturnDto>(product);
    }
        
      [HttpGet("brands")]
        public async Task<ActionResult<ProductBrand>> GetProductBrands()
        {
            return Ok(await _productsBrandRepo.ListAllAsync());
        }
      [HttpGet("types")]
        public async Task<ActionResult<ProductType>> GetProductTypes()
        {
            return Ok(await _productsTypeRepo.ListAllAsync());
        }
  }
    
}