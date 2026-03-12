using System.ComponentModel.DataAnnotations;

public class CreateUserDto :LoginUserDto
{
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [StringLength(50)]
    public string? LastName { get; set; }
}