﻿using ClothesStoreMobileApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using ClothesStoreMobileApplication.Service;
using ClothesStoreMobileApplication.ViewModels.User;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Numerics;
using Microsoft.AspNetCore.Authorization;

namespace ClothesStoreMobileApplication.Controllers
{
    [AllowAnonymous]
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
                return BadRequest(new { Message = "Invalid email." });
            }

            int otp = new Random().Next(100000, 999999);

            string token = KeyHelper.GenerateJwtToken(email.Email, otp);

            SendOtpToEmail(email.Email, otp);

            return Ok(new { Token = token, Message = "OTP sent to email." });
        }


        [HttpPost("verify-otp")]
        public IActionResult VerifyOtp([FromBody] OtpModel otpModel)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var claimsPrincipal = ValidateJwtToken(token);

            if (claimsPrincipal == null)
            {
                return Unauthorized(new { Message = "Invalid or expired token." });
            }

            var emailClaim = claimsPrincipal.FindFirst("Email")?.Value;
            var otpClaim = claimsPrincipal.FindFirst("OTP")?.Value;

            if (otpClaim == null || otpModel.Otp.ToString() != otpClaim)
            {
                return BadRequest(new { Message = "OTP does not match." });
            }

            string newToken = KeyHelper.GenerateJwtToken(emailClaim, 8020);

            return Ok(new { Token = newToken, Message = "OTP verified." });
        }

        public static ClaimsPrincipal ValidateJwtToken(string token)
        {
            var rsa = KeyHelper.GetPublicKey();
            var key = new RsaSecurityKey(rsa);

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "RaiYugi",
                    ValidAudience = "Saint",
                    IssuerSigningKey = key,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return principal;
            }
            catch
            {
                return null;
            }
        }


        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var claimsPrincipal = ValidateJwtToken(token);

            if (claimsPrincipal == null)
            {
                return Unauthorized(new { Message = "Invalid or expired token." });
            }

            var emailClaim = claimsPrincipal.FindFirst("Email")?.Value;
            var otpClaim = claimsPrincipal.FindFirst("OTP")?.Value;

            if (otpClaim != "8020")
            {
                return BadRequest(new { Message = "Invalid token." });
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == emailClaim);
            if (user == null)
            {
                return BadRequest(new { Message = "User not found." });
            }

            // user.Password = PasswordHelper.HashPassword(model.NewPassword);
            user.Password = model.NewPassword;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Password reset successfully." });
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
