using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SchoolAppCoreMVC.Models;

public class Enrollment
{
    public int EnrollmentID { get; set; }
    [Display(Name = "Course")]
    [Range(1, int.MaxValue)]
    public int CourseID { get; set; }

    [Display(Name = "Student")]
    [Range(1, int.MaxValue)]
    public int StudentID { get; set; }

    [Display(Name = "Grade")]
    [Precision(18, 2)]
    public decimal Grade { get; set; }

    public Course? Course { get; set; }
    public Student? Student { get; set; }
}
