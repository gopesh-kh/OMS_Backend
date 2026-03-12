using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using OMS_Backend.Models;
using OMS_Backend.Repositories;
using OMS_Backend.utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OMS_Backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IAuthRepository authRepository,
            IMapper mapper, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        public async Task<string?> RegisterAsync(CreateUserDto request)
        {
            if (request == null) return null;

            var existingUser = await _authRepository.UserExistAsync(request.Email);

            if (existingUser != null)
            {
                return null;
            }

            User user = new();

            user.Email = request.Email.ToLower();

            user.FirstName = request.FirstName.ToLower();

            if (request.LastName != null) user.LastName = request.LastName;

            if (!PasswordChecker.IsPasswordStrong(request.Password)) return null;
            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, request.Password);

            user.CreatedBy = request.Email;
            user.CreatedAt = DateTime.UtcNow;

            await _authRepository.RegisterAsync(user);

            var token = CreateToken(user);
            return token;
        }

        public async Task<string?> LoginAsync(LoginUserDto request)
        {
            if (request == null) return null;

            var user = await _authRepository.UserExistAsync(request.Email);

            if (user == null)
                return null;

            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
                return null;

            var token = CreateToken(user);
            return token;
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Name, user.FirstName)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Appsettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("Appsettings:Issuer"),
                audience: _configuration.GetValue<string>("Appsettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
                );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            return token;
        }
    }
}