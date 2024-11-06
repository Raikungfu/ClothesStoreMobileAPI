using ClothesStoreMobileApplication.Models;
using ClothesStoreMobileApplication.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using ClothesStoreMobileApplication.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Net;

namespace ClothesStoreMobileApplication.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ClothesStoreContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ClothesStoreContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModels model)
        {
            if (await _context.Users.AnyAsync(u => u.Username == model.Username))
                return BadRequest(new { Message = "Username is already taken." });

   //         var hashedPassword = PasswordHelper.HashPassword(model.Password);

            var user = new User
            {
                Username = model.Username,
    //            Password = hashedPassword,
                Password = model.Password,
                Email = model.Email,
                Phone = model.Phone,
                UserType = model.UserType
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            switch (user.UserType)
            {
                case UserType.Admin:
                    var admin = new Admin
                    {
                        UserId = user.UserId
                    };
                    _context.Admins.Add(admin);
                    await _context.SaveChangesAsync();

                    break;
                case UserType.Seller:
                    var seller = new Seller
                    {
                        UserId = user.UserId
                    };
                    _context.Sellers.Add(seller);
                    await _context.SaveChangesAsync();

                    break;
                case UserType.Customer:
                    var customer = new Customer
                    {
                        UserId = user.UserId
                    };
                    _context.Customers.Add(customer);
                    await _context.SaveChangesAsync();

                    break;
                default:
                    return BadRequest(new { Message = "Role of user not correct!" });
            }

            SendRegisterSuccessEmail(model.Email, model.Username, model.Password);

            return Ok(new { Message = "Registration successful." });
        }

        // Login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModels model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
            //  if (user == null || !PasswordHelper.VerifyPassword(model.Password, user.Password)) 
            if (user == null || user.Password != model.Password)
                return Unauthorized(new { Message = "Invalid credentials." });

            var token = GenerateJwtToken(user);

            return Ok(new { Token = token, Message = "Login successful!" });
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim("Id", user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, user.UserType.ToString())
            };

            var key = new RsaSecurityKey(KeyHelper.GetPrivateKey());
            var creds = new SigningCredentials(key, SecurityAlgorithms.RsaSha256);

            var token = new JwtSecurityToken(
                issuer: "RaiYugi",
                audience: "Saint",
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet("user/{type}")]
        public async Task<IActionResult> GetUserByType(UserType type)
        {
            var users = await _context.Users.Where(u => u.UserType == type).ToListAsync();
            return Ok(users);
        }

        [HttpGet("GetUserNameAndAvt")]
        public async Task<IActionResult> GetUserNameAndAvt()
        {
            var claimValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(claimValue) || !int.TryParse(claimValue, out int userId))
            {
                return Unauthorized("User not logged in. Please log in to continue.");
            }

            var user = (from u in _context.Users
                         where u.UserId == userId
                         join s in _context.Sellers on u.UserId equals s.UserId into sellerGroup
                         from seller in sellerGroup.DefaultIfEmpty()
                         join c in _context.Customers on u.UserId equals c.UserId into customerGroup
                         from customer in customerGroup.DefaultIfEmpty()
                         join a in _context.Admins on u.UserId equals a.UserId into adminGroup
                         from admin in adminGroup.DefaultIfEmpty()
                         select new
                         {
                             Name = u.UserType == UserType.Seller ? seller.CompanyName
                                  : u.UserType == UserType.Customer ? customer.Name
                                  : u.Username,
                             Avatar = u.UserType == UserType.Seller ? seller.Avt
                                    : u.UserType == UserType.Customer ? customer.Avt
                                    : admin.Avt,
                            Cover = u.UserType == UserType.Admin ? admin.Cover 
                            : u.UserType == UserType.Seller ? seller.Cover 
                            : null,
                            u.Email,
                            u.Phone,
                            u.UserType
                         }).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("GetProfile")]
        public async Task<IActionResult> GetProfile()
        {
            var claimValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(claimValue) || !int.TryParse(claimValue, out int userId))
            {
                return Unauthorized("User not logged in. Please log in to continue.");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
            {
                return NotFound();
            }

            switch (user.UserType)
            {
                case UserType.Admin:
                    var admin = await _context.Admins.Select(p => new
                    {
                        p.AdminId,
                        p.UserId,
                        p.Avt,
                        p.Cover
                    }).FirstOrDefaultAsync(u => u.UserId == userId);
                    return Ok(new
                    {
                        User = user,
                        Admin = admin
                    });
                case UserType.Seller:
                    var seller = await _context.Sellers.Select(p => new
                    {
                        p.SellerId,
                        p.CompanyName,
                        p.Address,
                        p.Description,
                        p.UserId,
                        p.Avt,
                        p.Cover,
                    }).FirstOrDefaultAsync(u => u.UserId == userId);
                    return Ok(new
                    {
                        User = user,
                        Seller = seller
                    });
                case UserType.Customer:
                    var customer = await _context.Customers.Select(p => new
                    {
                        p.UserId,
                        p.CustomerId,
                        p.Name,
                        p.Address,
                        p.Avt,
                    }).FirstOrDefaultAsync(u => u.UserId == userId);
                    return Ok(new
                    {
                        User = user,
                        Customer = customer
                    });
                default:
                    return BadRequest(new { Message = "Role of user not correct!" });
            }
        }

        [HttpPost("Customer/{userId}")]
        public async Task<IActionResult> UpdateCustomer(int userId, CustomerUpdateViewModel customer)
        {
            var customerInDb = await _context.Customers.FirstOrDefaultAsync(u => u.UserId == userId);

            if (customerInDb == null)
                return NotFound();

            customerInDb.Address = customer.Address;
            customerInDb.Name = customer.Name;
            customerInDb.Avt = customer.Avt;

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Update successful!" });
        }

        private void SendRegisterSuccessEmail(string email, string username, string password)
        {
            var fromEmail = new MailAddress(_configuration["EmailSettings:Username"], _configuration["EmailSettings:FromName"]);
            var toEmail = new MailAddress(email);
            string subject = "Registration Successful - Clothes Store";
            string body = string.Format(
                @"<!DOCTYPE html>
        <html lang='en'>
        <head>
            <meta charset='UTF-8'>
            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
            <title>Registration Successful - Clothes Store</title>
        </head>
        <body>
            <h2>Registration Successful!</h2>
            <p>Hello <strong>{0},</strong></p>
            <p>Congratulations on successfully registering on Clothes Store!</p>
            <p>Your username: <strong>{0}</strong></p>
            <p>Your password: <strong>{1}</strong></p>
            <p>Thank you for choosing our service!</p>
            <p>Best regards,</p>
            <p style='font-style: italic; font-weight: bold;'>The Clothes Store Team</p>
        </body>
        </html>
        <h3 style='color:red;'>Clothes Store - Fashion at its Best</h3>
        <p style='font-style: italic;'>Quality - Style - Speed - Convenience</p>
        </body>
        </html>", username, password);

            var smtp = new SmtpClient
            {
                Host = _configuration["EmailSettings:Host"],
                Port = int.Parse(_configuration["EmailSettings:Port"]),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, _configuration["EmailSettings:Password"])
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                smtp.Send(message);
            }
        }

    }
}
