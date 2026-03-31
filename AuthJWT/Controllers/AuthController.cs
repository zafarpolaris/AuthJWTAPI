using AuthJWT.Models;
using AuthJWT.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwtService;

        public AuthController(AppDbContext context)
        {
            _context = context;
            _jwtService = new JwtService();
        }

        [HttpPost("login")]
        public IActionResult Login(LoginModel model)
        {
            UserModel user = _context.Users
                .FirstOrDefault(u => u.Username == model.Username && u.PasswordHash == model.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            string accessToken = _jwtService.GenerateAccessToken(user.Username);
            string refreshToken = _jwtService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            _context.SaveChanges();

            TokenResponse response = new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return Ok(response);
        }

        [HttpPost("refresh")]
        public IActionResult Refresh(TokenResponse token)
        {
            UserModel user = _context.Users
                .FirstOrDefault(u => u.RefreshToken == token.RefreshToken);

            if (user == null || user.RefreshTokenExpiryTime < DateTime.UtcNow)
            {
                return Unauthorized();
            }

            string newAccessToken = _jwtService.GenerateAccessToken(user.Username);
            string newRefreshToken = _jwtService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            _context.SaveChanges();

            return Ok(new TokenResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }
    }
}
