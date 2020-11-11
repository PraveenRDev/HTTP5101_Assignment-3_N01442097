using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolProject.Models
{
    public class Teacher
    {
        // Fields that define a Teacher
        public string EmployeeNumber;
        public DateTime HireDate;
        public decimal Salary;
        public string TeacherFname;
        public string TeacherLName;
        public int TeacherId;
        public List<Class> Classes;
    }
}