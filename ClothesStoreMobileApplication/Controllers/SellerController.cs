using ClothesStoreMobileApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ClothesStoreMobileApplication.Controllers
{

    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {

        private readonly ClothesStoreContext _context;
        private readonly IConfiguration _configuration;

        public SellerController(ClothesStoreContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSeller(int id)
        {
            var seller = await _context.Sellers
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.SellerId == id);

            if (seller == null)
                return NotFound(new { Message = "Seller not found." });

            return Ok(seller);
        }
    }
}