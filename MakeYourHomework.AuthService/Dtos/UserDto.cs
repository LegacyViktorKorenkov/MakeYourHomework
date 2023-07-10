namespace MakeYourHomework.AuthService.Dtos;

public class UserDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Nickname { get; set; }
    public string Email { get; set; }
    public UserType UserType { get; set; }
}
