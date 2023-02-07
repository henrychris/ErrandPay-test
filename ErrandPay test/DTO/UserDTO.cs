using System.ComponentModel.DataAnnotations;

public class UserDTO
{
    [Required]
    // should the defaults be string.empty?
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}