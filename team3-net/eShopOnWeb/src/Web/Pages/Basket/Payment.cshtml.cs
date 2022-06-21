using System.Net;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Exceptions;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.eShopWeb.Web.Interfaces;
using Newtonsoft.Json;

namespace Microsoft.eShopWeb.Web.Pages.Basket
{
    public class PaymentModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IBasketService _basketService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IOrderService _orderService;
        private string _username;
        public string Error { get; set; }
        private readonly IBasketViewModelService _basketViewModelService;
        private readonly IAppLogger<PaymentModel> _logger;

        public PaymentModel(IBasketService basketService,
            IBasketViewModelService basketViewModelService,
            SignInManager<ApplicationUser> signInManager,
            IOrderService orderService,
            IAppLogger<PaymentModel> logger,
            IHttpClientFactory httpClientFactory)
        {
            _basketService = basketService;
            _signInManager = signInManager;
            _orderService = orderService;
            _basketViewModelService = basketViewModelService;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public BasketViewModel BasketModel { get; set; } = new BasketViewModel();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string cardNumber, int month, int year, int cvc)
        {
            try
            {
                await SetBasketModelAsync();

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var response = await Pay(cardNumber, month, year, cvc);

                if (response.IsSuccessStatusCode)
                {
                    await _orderService.CreateOrderAsync(BasketModel.Id, new Address("123 Main St.", "Kent", "OH", "United States", "44240"));
                    await _basketService.DeleteBasketAsync(BasketModel.Id);
                    return RedirectToPage("Success");
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    Error = "Credit card not found, please check your entry and try again";
                    return RedirectToPage("Payment", new { Error = this.Error });
                }
                else
                {
                    Error = "Something went wrong, check your credit card balance and try again";
                    return RedirectToPage("Payment", new { Error = this.Error });
                }
            }
            catch (EmptyBasketOnCheckoutException emptyBasketOnCheckoutException)
            {
                //Redirect to Empty Basket page
                _logger.LogWarning(emptyBasketOnCheckoutException.Message);
                return RedirectToPage("/Basket/Index");
            }
        }

        private async Task SetBasketModelAsync()
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                _username = User.Identity.Name;
                BasketModel = await _basketViewModelService.GetOrCreateBasketForUser(User.Identity.Name);
            }
            else
            {
                GetOrSetBasketCookieAndUserName();
                BasketModel = await _basketViewModelService.GetOrCreateBasketForUser(_username);
            }
        }

        private void GetOrSetBasketCookieAndUserName()
        {
            if (Request.Cookies.ContainsKey(Constants.BASKET_COOKIENAME))
            {
                _username = Request.Cookies[Constants.BASKET_COOKIENAME];
            }
            if (_username != null) return;

            _username = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Today.AddYears(10);
            Response.Cookies.Append(Constants.BASKET_COOKIENAME, _username, cookieOptions);
        }

        private async Task<HttpResponseMessage> Pay(string cardNumber, int month, int year, int cvc)
        {

            var order = new OrderModel()
            {
                UserName = _username,
                Date = DateTime.Now,
                Price = BasketModel.Total(),
                CreditCard = new CreditCardModel()
                {
                    CardNumber = cardNumber,
                    Cvc = cvc,
                    DateOfExpire = new DateTime(2000 + year, month, 1)
                }
            };

            var data = JsonConvert.SerializeObject(order);
            var url = "http://localhost:8000/api/Orders";
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            return await client.PostAsync(url, content);
        }
    }
}
