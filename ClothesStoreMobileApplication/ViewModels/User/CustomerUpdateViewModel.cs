using System.ComponentModel.DataAnnotations;

namespace ClothesStoreMobileApplication.ViewModels.User
{
    public class CustomerUpdateViewModel
    {
        [MaxLength(50)]
        public string? Name { get; set; }

        [MaxLength(255)]
        public string? Address { get; set; }

        public string? Avt { get; set; }
    }
}
