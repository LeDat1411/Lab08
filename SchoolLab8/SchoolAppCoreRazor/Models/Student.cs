using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SchoolAppCoreRazor.Models;

public class Student
{
    public int StudentID { get; set; }
    [Display(Name = "Last Name")]
    [Required]
    public required string LastName { get; set; }

    [Display(Name = "First Name")]
    [Required]
    public required string FirstName { get; set; }

    [Display(Name = "Enrollment Date")]
    [DataType(DataType.Date)]
    public DateTime EnrollmentDate { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; } = [];
}
