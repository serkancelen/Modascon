using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestParameters
{
    public class ProductRequestParameters : RequestParameters
    {
        public int CategoryId { get; set; }
        public decimal MinPrice { get; set; } = 0;
        public decimal MaxPrice { get; set; } = int.MaxValue;
        public bool IsValidPrice => MaxPrice > MinPrice;
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public ProductRequestParameters() : this(1,12)
        {
            
        }
        public ProductRequestParameters(int pageNumber =1, int pageSize = 12)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
