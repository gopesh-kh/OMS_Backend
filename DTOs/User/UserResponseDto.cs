namespace OMS_Backend.DTOs.User
{
    public class UserResponseDto
    {
        public int UserId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string? LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}".Trim();

        public string Email { get; set; } = string.Empty;

        public int UserRoleId { get; set; }

        public string? UserRoleName { get; set; }
    }
}