using System.ComponentModel.DataAnnotations;

namespace MakeYourHomework.HomeworkService.Models;

public class User
{
    [Key]
    public string Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Nickname { get; set; }
    [Required]
    public string Email { get; set; }
    public UserType UserType { get; set; }
    public ICollection<User> Users { get; set; }
    public ICollection<Homework> Homeworks { get; set; }
}
