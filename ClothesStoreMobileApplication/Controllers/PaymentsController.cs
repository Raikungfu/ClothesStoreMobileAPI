using Braintree;
using ClothesStoreMobileApplication.Library;
using ClothesStoreMobileApplication.Service;
using ClothesStoreMobileApplication.ViewModels.Payment;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;

namespace ClothesStoreMobileApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public PaymentsController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpPost("get-access-token")]
        public async Task<IActionResult> GetAccessToken()
        {
            var clientID = _configuration["Paypal:ClientId"];
            var secretID = _configuration["Paypal:SecretId"];
            var authString = $"{clientID}:{secretID}";
            var encodedAuthString = Convert.ToBase64String(Encoding.UTF8.GetBytes(authString));

            using var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedAuthString);

            var content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await client.PostAsync("https://api-m.sandbox.paypal.com/v1/oauth2/token", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();

                return Ok(new { ClientToken = jsonResponse.AccessToken });
            }

            return BadRequest("Unable to fetch access token");
        }

        [HttpPost("create-vnpay-payment-link")]
        public async Task<IActionResult> CreatePaymentUrl(VnPaymentRequestModel model)
        {
            string vnp_ReturnUrl = _configuration["VnPay:PaymentBackReturnUrl"];
            string vnp_Url = _configuration["VnPay:BaseURL"];
            string vnp_TmnCode = _configuration["VnPay:TmnCode"];
            string vnp_HashSecret = _configuration["VnPay:HashSecret"];

            VnPayLibrary vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", "2.1.0");
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (model.TotalPrice * 100).ToString());

            if (model.VnPayMethod == VnPayMethod.ATM)
            {
                vnpay.AddRequestData("vnp_BankCode", "VNBANK");
            }
            else if (model.VnPayMethod == VnPayMethod.CreditCard)
            {
                vnpay.AddRequestData("vnp_BankCode", "INTCARD");
            }

            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(HttpContext));
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + model.BookingId);
            vnpay.AddRequestData("vnp_OrderType", "other");
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_ReturnUrl);
            vnpay.AddRequestData("vnp_TxnRef", model.BookingId.ToString());

            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            return Ok(new { paymentUrl = paymentUrl });
        }


        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequest orderRequest)
        {
            var client = _httpClientFactory.CreateClient();
            var requestContent = new StringContent(JsonConvert.SerializeObject(orderRequest), Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", orderRequest.AccessToken);

            var response = await client.PostAsync("https://api-m.sandbox.paypal.com/v2/checkout/orders", requestContent);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return Ok(jsonResponse);
            }

            return BadRequest("Error creating order");
        }

        [HttpPost("capture-order/{orderId}")]
        public async Task<IActionResult> CaptureOrder(string orderId, [FromBody] CaptureRequest captureRequest)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsync($"https://api-m.sandbox.paypal.com/v2/checkout/orders/{orderId}/capture", new StringContent("{}", Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return Ok(jsonResponse);
            }

            return BadRequest("Error capturing order");
        }
    }

    public class TokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
    }

    public class OrderRequest
    {
        public string AccessToken { get; set; }
        public string Amount { get; set; }
    }

    public class CaptureRequest
    {
        public string AccessToken { get; set; }
    }

}
