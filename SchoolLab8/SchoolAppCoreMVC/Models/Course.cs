using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SchoolAppCoreMVC.Models;

public class Course
{
    public int CourseID { get; set; }
    [Display(Name = "Title")]
    [Required]
    public required string Title { get; set; }

    [Display(Name = "Credits")]
    public int Credits { get; set; }

    [Display(Name = "Department")]
    [Range(1, int.MaxValue)]
    public int DepartmentID { get; set; }

    public Department? Department { get; set; }
    public ICollection<Enrollment> Enrollments { get; set; } = [];
}
