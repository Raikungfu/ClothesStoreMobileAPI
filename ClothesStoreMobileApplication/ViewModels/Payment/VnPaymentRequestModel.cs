namespace ClothesStoreMobileApplication.ViewModels.Payment
{
    public enum VnPayMethod
    {
        ATM = 0,
        QRCode = 1,
        CreditCard = 2
    }

    public class VnPaymentRequestModel
    {
        public int BookingId { get; set; }
        public string? FullName { get; set; }
        public string? Description { get; set; }
        public double? TotalPrice { get; set; }
        public DateTime CreateDate { get; set; }
        public VnPayMethod VnPayMethod { get; set; }
    }
}
