namespace PriceNegotiationAPI.Application.User.Dto;

public class RegisterUserDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int Role { get; set; }
}