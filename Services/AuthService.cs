using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using OMS_Backend.Models;
using OMS_Backend.Repositories;
using OMS_Backend.utils;
using OMS_Backend.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OMS_Backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthService(
            IAuthRepository authRepository,
            IConfiguration configuration,
            IMapper mapper)
        {
            _authRepository = authRepository;
            _configuration = configuration;
            _mapper = mapper;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<string?> RegisterAsync(CreateUserDto request)
        {
            if (Guard.IsNull(request) || Guard.IsNullOrEmpty(request.Email) || Guard.IsNullOrEmpty(request.Password))
                return null;

            var existingUser = await _authRepository.UserExistAsync(request.Email);

            if (!Guard.IsNull(existingUser) || !PasswordChecker.IsPasswordStrong(request.Password))
                return null;

            var user = _mapper.Map<User>(request);

            user.Email = user.Email.ToLower();
            user.FirstName = user.FirstName.ToLower();
            user.CreatedBy = request.Email;
            user.CreatedAt = DateTime.UtcNow;
            user.UserRoleId = 3;

            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

            await _authRepository.RegisterAsync(user);

            return CreateToken(user);
        }

        public async Task<string?> LoginAsync(LoginUserDto request)
        {
            if (Guard.IsNull(request) || Guard.IsNullOrEmpty(request.Email) || Guard.IsNullOrEmpty(request.Password))
                return null;

            var user = await _authRepository.UserExistAsync(request.Email);

            if (Guard.IsNull(user))
                return null;

            var verifyResult = _passwordHasher.VerifyHashedPassword(
                user!,
                user!.PasswordHash,
                request.Password);

            if (verifyResult == PasswordVerificationResult.Failed)
                return null;

            return CreateToken(user);
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Name, user.FirstName),
                new(ClaimTypes.NameIdentifier, user.UserId.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["AppSettings:Token"]!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration["AppSettings:Issuer"],
                audience: _configuration["AppSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}