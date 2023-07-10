using System.ComponentModel.DataAnnotations;

namespace MakeYourHomework.HomeworkService.Models;

public class Homework
{
    [Key]
    public int Id { get; set; }
    public ICollection<Assigment> Assigments { get; set; }
    [Required]
    public DateTime Date { get; set; }
}
