using Entities.Models;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System.Diagnostics;

namespace Modascon.Controllers
{
    public class OrderController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly Cart _cart;

        public OrderController(IServiceManager manager, Cart cart)
        {
            _manager = manager;
            _cart = cart;
        }

        [Authorize]
        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout([FromForm]Order order)
        {
            if (_cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", " Aktif sipariş bulunmamaktadır");
            }
            if (ModelState.IsValid)
            {
                //order.Cart = _cart;
                //var payment = ProcessPayment(order);
                //if(payment.Status == "success")
                //{
                    
                //}
                //order.Cart = _cart;
                //return View(order);
                order.Lines = _cart.Lines.ToArray();
                _manager.OrderService.SaveOrder(order);
                _cart.Clear();
                return RedirectToPage("/Complete", new { OrderId = order.OrderId });

            }
            else
            {
                return View();
            }
        }

        //private Payment ProcessPayment(Order order, IdentityUser user)
        //{
        //    Options options = new Options();
        //    options.ApiKey = "sandbox-lRQqf62NqsYU9d8p7LcSP8qAtzSwWAzm";
        //    options.SecretKey = "sandbox-BXrML1emoze7d88tsVYQD2OuJDVu3m0U";
        //    options.BaseUrl = "https://sandbox-api.iyzipay.com";

        //    CreatePaymentRequest request = new CreatePaymentRequest();
        //    request.Locale = Locale.TR.ToString();
        //    request.ConversationId = new Random().Next(111111111,999999999).ToString();
        //    request.Price = order.Cart.ComputeTotalValue().ToString();
        //    request.PaidPrice = order.Cart.ComputeTotalValue().ToString();
        //    request.Currency = Currency.TRY.ToString();
        //    request.Installment = 1;
        //    request.PaymentChannel = PaymentChannel.WEB.ToString();
        //    request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

        //    PaymentCard paymentCard = new PaymentCard();
        //    paymentCard.CardHolderName = order.CartName;
        //    paymentCard.CardNumber = order.CartNumber;
        //    paymentCard.ExpireMonth = order.ExpirationMonth;
        //    paymentCard.ExpireYear = order.ExpirationYear;
        //    paymentCard.Cvc = order.Cvc;
        //    paymentCard.RegisterCard = 0;
        //    request.PaymentCard = paymentCard;

        //    Buyer buyer = new Buyer();
        //    buyer.Id = user.Id;
        //    buyer.Name = user.UserName;
        //    buyer.GsmNumber = user.PhoneNumber.ToString();
        //    buyer.Email = user.Email;
        //    buyer.IdentityNumber = "74300864791";
        //    buyer.LastLoginDate = "2015-10-05 12:43:35";
        //    buyer.RegistrationDate = "2013-04-21 15:12:09";
        //    buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
        //    buyer.Ip = "85.34.78.112";
        //    buyer.City = "Istanbul";
        //    buyer.Country = "Turkey";
        //    buyer.ZipCode = "34732";
        //    request.Buyer = buyer;

        //    Address shippingAddress = new Address();
        //    shippingAddress.ContactName = order.Name;
        //    shippingAddress.City = order.City;
        //    shippingAddress.Country = order.City;
        //    shippingAddress.Description = order.Line1 + order.Line2 + order.Line3;
        //    shippingAddress.ZipCode = "34742";
        //    request.ShippingAddress = shippingAddress;

        //    Address billingAddress = new Address();
        //    billingAddress.ContactName = order.Name;
        //    billingAddress.City = "Istanbul";
        //    billingAddress.Country = "Turkey";
        //    billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
        //    billingAddress.ZipCode = "34742";
        //    request.BillingAddress = billingAddress;

        //    List<BasketItem> basketItems = new List<BasketItem>();

        //    foreach (var item in order.Cart.Lines ?? Enumerable.Empty<CartLine>())
        //    {
        //        BasketItem firstBasketItem = new BasketItem();
        //        firstBasketItem.Id = item.Product.Id.ToString();
        //        firstBasketItem.Name = item.Product.ProductName;
        //        firstBasketItem.Category1 = item.Product.CategoryId.ToString();
        //        firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
        //        firstBasketItem.Price = item.Product.Price.ToString();
        //        basketItems.Add(firstBasketItem);
        //    }

            

        //    request.BasketItems = basketItems;

        //    Payment payment = Payment.Create(request, options);
        //    return payment;
        //}
    }
}
