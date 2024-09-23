using ClothesStoreMobileApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using ClothesStoreMobileApplication.Service;
using ClothesStoreMobileApplication.ViewModels.User;

namespace ClothesStoreMobileApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForgotController : ControllerBase
    {
        private readonly ClothesStoreContext _context;
        private readonly IConfiguration _configuration;

        public ForgotController(ClothesStoreContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] EmailModel email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email.Email);
            if (user == null || !IsValidEmail(email.Email))
            {
                return BadRequest("Invalid email.");
            }

            int otp = new Random().Next(100000, 999999);
            HttpContext.Session.SetInt32("otp", otp);
            HttpContext.Session.SetString("email", email.Email);

            SendOtpToEmail(email.Email, otp);

            return Ok("OTP sent to email.");
        }

        [HttpPost("verify-otp")]
        public IActionResult VerifyOtp([FromBody] OtpModel otp)
        {
            int? storedOtp = HttpContext.Session.GetInt32("otp");
            string email = HttpContext.Session.GetString("email");

            if (storedOtp == null || otp.Otp != storedOtp)
            {
                return BadRequest("OTP does not match.");
            }

            return Ok("OTP verified.");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            string email = HttpContext.Session.GetString("email");
            if (string.IsNullOrEmpty(email) || model.NewPassword != model.ConfirmPassword)
            {
                return BadRequest("Password does not match or invalid session.");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            user.Password = PasswordHelper.HashPassword(model.NewPassword);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok("Password reset successfully.");
        }

        private void SendOtpToEmail(string email, int otp)
        {
            var fromEmail = new MailAddress(_configuration["EmailSettings:Username"], _configuration["EmailSettings:FromName"]);
            var toEmail = new MailAddress(email);
            string subject = "Reset Password OTP";
            string body = $"Hi,<br/>Your OTP for password reset is: <strong>{otp}</strong>";

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
    public class EmailModel
    {
        public string Email { get; set; }
    }

    public class OtpModel
    {
        public int Otp { get; set; }
    }
}
