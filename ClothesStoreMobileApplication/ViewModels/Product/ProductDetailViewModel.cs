using ClothesStoreMobileApplication.ViewModels.Option;
using ClothesStoreMobileApplication.ViewModels.Review;
using ClothesStoreMobileApplication.ViewModels.User;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace ClothesStoreMobileApplication.ViewModels.Product
{
    public class ProductDetailViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public uint? NewPrice { get; set; }
        public uint? OldPrice { get; set; }
        public uint? QuantitySold { get; set; }
        public int? CategoryId { get; set; }
        public int? SellerId { get; set; }
        public double RatingPoint { get; set; }
        public int RatingCount { get; set; }
        public List<OptionModel> Options { get; set; }
        public SellerModel Seller { get; set; }
        public List<ReviewModel> Reviews { get; set; }
    }
}
