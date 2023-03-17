using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.DAL.Entities;

namespace Talabat.BLL.Specification.ProductSpecification
{
    public class ProductWithFiltersForCountSpec:BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpec(ProductParams productParams)
           : base(p => (productParams.brandId == null || p.ProductBrandId == productParams.brandId)
           &&(productParams.typeId == null || p.ProductTypeId == productParams.typeId) 
           &&((string.IsNullOrEmpty(productParams.Search)) || p.Name.ToLower().Contains(productParams.Search)))
        {
        }
    }
}
