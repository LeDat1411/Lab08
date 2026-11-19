using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SchoolAppCoreMVC.Models;

public class Instructor
{
    public int InstructorID { get; set; }
    [Display(Name = "Last Name")]
    [Required]
    public required string LastName { get; set; }

    [Display(Name = "First Name")]
    [Required]
    public required string FirstName { get; set; }

    [Display(Name = "Hire Date")]
    [DataType(DataType.Date)]
    public DateTime HireDate { get; set; }
}
