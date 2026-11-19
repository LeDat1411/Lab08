using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SchoolAppCoreRazor.Models;

public class Department
{
    public int DepartmentID { get; set; }
    [Display(Name = "Department Name")]
    [Required]
    public required string DepartmentName { get; set; }

    [Display(Name = "Budget")]
    [Precision(18, 2)]
    public decimal Budget { get; set; }

    [Display(Name = "Start Date")]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    public ICollection<Course> Courses { get; set; } = [];
}
