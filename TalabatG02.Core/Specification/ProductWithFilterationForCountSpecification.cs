using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatG02.Core.Entities;

namespace TalabatG02.Core.Specification
{
    public class ProductWithFilterationForCountSpecification :BaseSpecification<Product>
    {
        public ProductWithFilterationForCountSpecification(ProductSpecParams productSpec)
            : base(p =>
            (!productSpec.BrandId.HasValue || p.ProductBrandId == productSpec.BrandId) &&
            (!productSpec.TypeId.HasValue || p.ProductTypeId == productSpec.TypeId)
            )
        {
          
        }
    }
}
