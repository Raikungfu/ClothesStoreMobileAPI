using Braintree;
using ClothesStoreMobileApplication.Service;
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
        /*
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout(decimal amount, string paymentNonce)
        {
            var result = await _braintreeService.ProcessPayment(amount, paymentNonce);
            if (result.IsSuccess())
            {
                return Ok(result.Target);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("client-token")]
        public async Task<IActionResult> GetClientToken()
        {
            var clientToken = await _braintreeService.GetClientToken();
            return Ok(new { ClientToken = clientToken });
        }

        */

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
