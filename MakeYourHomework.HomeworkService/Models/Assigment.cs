using System.ComponentModel.DataAnnotations;

namespace MakeYourHomework.HomeworkService.Models;

public class Assigment
{
    [Key]
    public int Id { get; set; }
    public string Task{ get; set; }
    public string Body { get; set; }
    public AssigmentType TaskType { get; set; }
}
