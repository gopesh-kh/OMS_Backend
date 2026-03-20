using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OMS_Backend.DTOs.Auth;
using OMS_Backend.DTOs.User;
using OMS_Backend.Models;
using OMS_Backend.Repositories;
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

        public async Task<AuthResponseDto> RegisterAsync(CreateUserDto request)
        {
            var email = request.Email.Trim().ToLower();

            if (await _authRepository.EmailExistsAsync(email))
                throw new Exception("User already exists");

            var user = _mapper.Map<User>(request);

            user.Email = email;
            user.FirstName = user.FirstName.Trim();
            user.LastName = user.LastName?.Trim()!;
            user.UserRoleId = 2;

            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

            var createdUser = await _authRepository.RegisterAsync(user);

            var token = CreateToken(createdUser);

            var userDto = _mapper.Map<UserResponseDto>(createdUser);

            return new AuthResponseDto
            {
                Token = token,
                User = userDto
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginUserDto request)
        {
            var email = request.Email.Trim().ToLower();

            var user = await _authRepository.GetByEmailAsync(email);

            if (user == null)
                throw new Exception("Invalid email or password");

            var result = _passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                request.Password);

            if (result == PasswordVerificationResult.Failed)
                throw new Exception("Invalid email or password");

            var token = CreateToken(user);

            var userDto = _mapper.Map<UserResponseDto>(user);

            return new AuthResponseDto
            {
                Token = token,
                User = userDto
            };
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Name, user.FirstName),
                new(ClaimTypes.Role, user.UserRole?.RoleName ?? "")
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["AppSettings:Token"]!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                issuer: _configuration["AppSettings:Issuer"],
                audience: _configuration["AppSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}