using Services.Contracts;

namespace Services.EFCore
{
    public class ServiceManager : IServiceManager
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IContactService _contactService;
        private readonly IOrderService _orderService;
        private readonly IAuthService _authService;

        public ServiceManager(IProductService productService, ICategoryService categoryService = null, IContactService contactService = null, IOrderService orderService = null)
        {
            _productService = productService;
            _categoryService = categoryService;
            _contactService = contactService;
            _orderService = orderService;
        }
        public ICategoryService CategoryService => _categoryService;

        public IProductService ProductService => _productService;

        public IContactService ContactService => _contactService;

        public IOrderService OrderService => _orderService;

        public IAuthService AuthService => _authService;
    }
}
