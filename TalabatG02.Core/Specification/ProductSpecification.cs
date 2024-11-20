using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatG02.Core.Entities;

namespace TalabatG02.Core.Specification
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        public ProductSpecification(ProductSpecParams productSpec)
            :base(p =>
            (string.IsNullOrEmpty(productSpec.Search) || p.Name.ToLower().Contains(productSpec.Search)) &&
            (!productSpec.BrandId.HasValue || p.ProductBrandId == productSpec.BrandId) &&
            (!productSpec.TypeId.HasValue || p.ProductTypeId == productSpec.TypeId)
            )
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);

            if (!string.IsNullOrEmpty(productSpec.Sort))
            {
                switch(productSpec.Sort)
                {
                    case "PriceAsc":
                        AddOraerBy(p =>p.Price); 
                        break;
                    case "PriceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOraerBy( p => p.Name);
                        break;
                }
            }
            
            ApplayPagination(productSpec.PageSize*(productSpec.PageIndex-1), productSpec.PageSize);
        }
        public ProductSpecification(int id)
            :base(p => p.Id == id)
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
        }
    }
}
