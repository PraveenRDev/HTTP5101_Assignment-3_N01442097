using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolProject.Models
{
    public class Teacher
    {
        // Teacher model with 'required' validation 

        [Required(ErrorMessage ="Enter Employee Number")]
        [Display(Name = "Employee Number")]
        [DataType(DataType.Text)]
        public string EmployeeNumber { get; set; }

        [Required(ErrorMessage = "Enter Hire Date")]
        [DataType(DataType.Date)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        [Required(ErrorMessage = "Enter Salary")]
        [DataType(DataType.Currency)]
        [Display(Name = "Salary")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "Enter First Name")]
        [Display(Name = "First Name")]
        [DataType(DataType.Text)]
        public string TeacherFname { get; set; }

        [Required(ErrorMessage = "Enter Last Name")]
        [Display(Name = "Last Name")]
        [DataType(DataType.Text)]
        public string TeacherLname { get; set; }

        public int TeacherId;

        public List<Class> Classes;
    }
}