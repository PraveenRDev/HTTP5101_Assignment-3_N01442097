using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolProject.Models;

namespace SchoolProject.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Teacher/List
        public ActionResult List()
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers();

            return View(Teachers);
        }

        //GET : /Teacher/Show/{id} -> /Teacher/Show/1
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            // Get teacher information
            Teacher Teacher = controller.GetTeacherInformation(id);
            // Get course information associated to the teacher
            Teacher.Classes = controller.GetTeacherClasses(id);
            return View(Teacher);
        }

    }
}