using Braintree;

namespace ClothesStoreMobileApplication.Service
{

    public class BraintreeService
    {
        private readonly IBraintreeGateway _gateway;

        public BraintreeService(IConfiguration configuration)
        {
            _gateway = new BraintreeGateway
            {
                Environment = Braintree.Environment.SANDBOX,
                MerchantId = configuration["Braintree:MerchantId"],
                PublicKey = configuration["Braintree:PublicKey"],
                PrivateKey = configuration["Braintree:PrivateKey"]
            };
        }

        public async Task<Result<Transaction>> ProcessPayment(decimal amount, string nonce)
        {
            var request = new TransactionRequest
            {
                Amount = amount,
                PaymentMethodNonce = nonce,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };

            return await _gateway.Transaction.SaleAsync(request);
        }

        public async Task<string> GetClientToken()
        {
            return await _gateway.ClientToken.GenerateAsync();
        }
    }

}
