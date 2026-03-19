using System.ComponentModel.DataAnnotations;

public class CreateUserDto : LoginUserDto
{
    [Required(ErrorMessage = "First name is required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters")]
    public string FirstName { get; set; } = string.Empty;

    [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "User role is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid user role")]
    public int UserRoleId { get; set; }
}
