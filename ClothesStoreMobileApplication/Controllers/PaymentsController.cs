using ClothesStoreMobileApplication.Service;
using Microsoft.AspNetCore.Mvc;

namespace ClothesStoreMobileApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly BraintreeService _braintreeService;

        public PaymentsController(BraintreeService braintreeService)
        {
            _braintreeService = braintreeService;
        }

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
    }

}
