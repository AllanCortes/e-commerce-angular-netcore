using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
  public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
  {
    public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productsParams)
      :base(x=>
      (string.IsNullOrEmpty(productsParams.Search) || x.Name.ToLower().Contains  (productsParams.Search)) &&
        (!productsParams.brandId.HasValue || x.ProductBrandId == productsParams.brandId) &&
        (!productsParams.typeId.HasValue || x.ProductTypeId == productsParams.typeId)
      )
    {
      AddInclude(x => x.ProductType);
      AddInclude(x => x.ProductBrand);
      AddOrderBy(x => x.Name);
      ApplyPaging(productsParams.PageSize * (productsParams.PageIndex - 1), productsParams.PageSize);

      if (!string.IsNullOrEmpty(productsParams.Sort))
            {
                switch (productsParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }

    }

    public ProductsWithTypesAndBrandsSpecification(int id) : base(x=>x.Id == id)
    {
      AddInclude(x => x.ProductType);
      AddInclude(x => x.ProductBrand);  
    }
  }
}