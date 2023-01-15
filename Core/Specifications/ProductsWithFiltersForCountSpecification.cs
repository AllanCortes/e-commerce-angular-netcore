using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductsWithFiltersForCountSpecification(ProductSpecParams productsParams) :base(x=>
        (string.IsNullOrEmpty(productsParams.Search) || x.Name.ToLower().Contains  (productsParams.Search))&&
        (!productsParams.brandId.HasValue || x.ProductBrandId == productsParams.brandId) &&
        (!productsParams.typeId.HasValue || x.ProductTypeId == productsParams.typeId))
        {
            
        }
        
    }
}