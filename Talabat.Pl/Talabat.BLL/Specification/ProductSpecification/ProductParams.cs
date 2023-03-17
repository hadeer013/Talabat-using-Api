using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.BLL.Specification.ProductSpecification
{
    public class ProductParams
    {
        private int pageSize = 5;

        public string sort { get; set; }

        private string search;
        public int? brandId { get; set; }
        public int? typeId { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get => pageSize; set => pageSize = value > 50 ? 50 : value; }
        public string Search { get => search; set => search = value.ToLower(); }
    }
}
