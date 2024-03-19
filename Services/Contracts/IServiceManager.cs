using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IServiceManager
    {
        ICategoryService CategoryService { get; }
        IProductService ProductService { get; }
        IContactService ContactService { get; }
        IOrderService OrderService { get; }
        IAuthService AuthService { get; }
    }
}
