using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClothesStoreMobileApplication.ViewModels.Order
{
    public class OrderCreateViewModel
    {
        public int CustomerId { get; set; }

        public string? ShipName { get; set; }

        public string? ShipMail { get; set; }

        public string? ShipPhone { get; set; }

        public string? ShipAddress { get; set; }

        public string? DiscountCode { get; set; }

        public string PaymentMethod { get; set; } = "direct";

    }
}
